﻿Imports Logica.AccesoLogica
Imports System.Globalization
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar

Public Class F0_ClasesPracticas3
#Region "ATRIBUTOS"
    Dim _dt As New DataTable

    Dim _dtFechas As New DataTable

    Dim _dtHorasLiberar As New DataTable

    Dim _meses As ClsMeses

    Dim _marcarManual As Integer

    Dim _i As Integer = 1

    Dim _marcando As Boolean

    Dim _mesVisto As Integer = 1

    Dim _AlumSelect As Integer

    Dim _limiteDias As Integer



    Public _numiAlumInscrito As Int16 = -1

    Dim _listColores As List(Of Color)

    'Dim _verDisponibilidad As Boolean

    Const conHoraDiligencia As Integer = -5
    Const conHoraLiberada As Integer = -4
    Const conCumple As Integer = -3
    Const conFeriado As Integer = -2
    Const conSabado As Integer = -1
    Const conDiaMarcado As Integer = 0
    Const conDiaGrabadoAlumno As Integer = 1

    Const conEstClaseFalta As Integer = -1
    Const conEstClaseProgramada As Integer = 0
    Const conEstClaseAsistida As Integer = 1
    Const conEstClasePermiso As Integer = 2
    Const conEstClaseSuspension As Integer = 3

    Private _cantClasesPracticas As Integer = 13
    Private _cantClasesReforzamiento As Integer = 5

    Private _isClasePractica As Boolean

    'Dim _cantClasesPracticas As Integer

#End Region

#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()

        'Dim fecha As DateTime =
        '_prCargarBuscador(New DateTime(2017, 1, 1))
        Me.Text = "P r o g r a m a c i ó n    d e    c l a s e s    p r a c t i c a s".ToUpper
        Me.WindowState = FormWindowState.Maximized

        '------------YA NO CARGO DEL NUMERO DE CLASES ESTATICAMENTE-------------------------------------------------
        'cargar numero de clases 
        ''Dim dtCantClassTeo As DataTable = L_prTCE000General()
        ''_cantClasesPracticas = dtCantClassTeo.Rows(0).Item("eprac")
        ''_cantClasesReforzamiento = dtCantClassTeo.Rows(0).Item("erefor")
        '------------YA NO CARGO DEL NUMERO DE CLASES ESTATICAMENTE-------------------------------------------------



        'cargar colores a la lista
        _prCargarListaColores()

        tbFechaSelect.Value = New Date(Now.Year, Now.Month, 1)

        _prCargarComboSucursal()

        _prCargarComboInstructores()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        LblPaginacion.Text = ""
        ''PanelFechas.Enabled = False


        _marcarManual = 0
        _dtFechas = New DataTable
        _dtFechas.Columns.Add("ehnumi", GetType(Integer))
        _dtFechas.Columns.Add("ehtser", GetType(Integer))
        _dtFechas.Columns.Add("ehlin", GetType(Integer))
        _dtFechas.Columns.Add("ehfec", GetType(Date))
        _dtFechas.Columns.Add("ehhor", GetType(String))
        _dtFechas.Columns.Add("ehest", GetType(Integer))
        _dtFechas.Columns.Add("ehobs", GetType(String))
        _dtFechas.Columns.Add("estado", GetType(Integer))

        _dtHorasLiberar = New DataTable
        _dtHorasLiberar.Columns.Add("ehhlin", GetType(Integer))
        _dtHorasLiberar.Columns.Add("ehhchof", GetType(Integer))
        _dtHorasLiberar.Columns.Add("ehhfec", GetType(Date))
        _dtHorasLiberar.Columns.Add("ehhhor", GetType(String))
        _dtHorasLiberar.Columns.Add("ehhobs", GetType(String))
        _dtHorasLiberar.Columns.Add("estado", GetType(Integer))

        'poner formato a la fecha
        tbFechaSelect.Format = DateTimePickerFormat.Custom
        tbFechaSelect.CustomFormat = "MMMM yyyy"

        _AlumSelect = -1
        '_limiteDias = 15
        _limiteDias = _cantClasesPracticas

        'poner eventos
        AddHandler tbFechaSelect.ValueChanged, AddressOf tbFechaSelect_ValueChanged

        'iniciando el cambio de numero de clases por sucursal
    End Sub
    Private Sub _prCargarListaColores()
        _listColores = New List(Of Color)
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100


        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100


        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100


        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100


        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100

        'mas colores repetidos
        _listColores.Add(_prGetColorFromHex("#FFCDD2")) 'red 100
        _listColores.Add(_prGetColorFromHex("#E1BEE7")) 'purple 100
        _listColores.Add(_prGetColorFromHex("#C5CAE9")) 'indigo 100
        _listColores.Add(_prGetColorFromHex("#80D8FF")) 'ligth blue A100
        _listColores.Add(_prGetColorFromHex("#1DE9B6")) 'teal A400
        _listColores.Add(_prGetColorFromHex("#CCFF90")) 'light green A100
        _listColores.Add(_prGetColorFromHex("#FFFF8D")) 'yellow A100
        _listColores.Add(_prGetColorFromHex("#FFAB40")) 'orange A200
        _listColores.Add(_prGetColorFromHex("#F48FB1")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#B39DDB")) 'deep purple 200
        _listColores.Add(_prGetColorFromHex("#90CAF9")) 'blue 200
        _listColores.Add(_prGetColorFromHex("#00B8D4")) 'cyan A700
        _listColores.Add(_prGetColorFromHex("#69F0AE")) 'green A200
        _listColores.Add(_prGetColorFromHex("#C6FF00")) 'lime A400
        _listColores.Add(_prGetColorFromHex("#FFAB00")) 'amber A700
        _listColores.Add(_prGetColorFromHex("#FF6E40")) 'Deep orange A200
        _listColores.Add(_prGetColorFromHex("#EF9A9A")) 'red 200
        _listColores.Add(_prGetColorFromHex("#CE93D8")) 'purple 200
        _listColores.Add(_prGetColorFromHex("#9FA8DA")) 'indigo 200
        _listColores.Add(_prGetColorFromHex("#00B0FF")) 'light blue A400
        _listColores.Add(_prGetColorFromHex("#00BFA5")) 'teal A700
        _listColores.Add(_prGetColorFromHex("#76FF03")) 'light green A400
        _listColores.Add(_prGetColorFromHex("#FF9100")) 'orange A400
        _listColores.Add(_prGetColorFromHex("#D7CCC8")) 'brown 100
        _listColores.Add(_prGetColorFromHex("#F8BBD0")) 'pink 200
        _listColores.Add(_prGetColorFromHex("#D1C4E9")) 'deep purple 100
        _listColores.Add(_prGetColorFromHex("#42A5F5")) 'blue 400
        _listColores.Add(_prGetColorFromHex("#00E5FF")) 'cyan A400
        _listColores.Add(_prGetColorFromHex("#00E676")) 'green A400
        _listColores.Add(_prGetColorFromHex("#F4FF81")) 'lime A100
        _listColores.Add(_prGetColorFromHex("#FFC400")) 'amber A400
        _listColores.Add(_prGetColorFromHex("#FF9E80")) 'deep orange A100
    End Sub

    Private Sub _prCargarComboSucursal()
        Dim dt As New DataTable
        dt = L_prSucursalAyuda()

        With tbSuc
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("canumi").Width = 70
            .DropDownList.Columns("canumi").Caption = "COD"

            .DropDownList.Columns.Add("cadesc").Width = 200
            .DropDownList.Columns("cadesc").Caption = "descripcion".ToUpper

            .ValueMember = "canumi"
            .DisplayMember = "cadesc"
            .DataSource = dt
            .Refresh()
        End With

        If dt.Rows.Count > 0 Then
            tbSuc.Value = gi_userSuc
            If gb_userTodasSuc = False Then
                tbSuc.ReadOnly = True
            End If

        End If

    End Sub

    Public Sub _prCargarComboInstructores()
        Dim dt As New DataTable
        dt = L_prPersonaAyudaGeneralPorSucursal(tbSuc.Value, gi_LibPERSTIPOInstructor) 'gi_userSuc

        With tbPersona
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("panumi").Width = 70
            .DropDownList.Columns("panumi").Caption = "COD"

            .DropDownList.Columns.Add("panom1").Width = 200
            .DropDownList.Columns("panom1").Caption = "NOMBRE COMPLETO"

            .ValueMember = "panumi"
            .DisplayMember = "panom1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarGridAlumnos(numiInst As String)
        Dim dt As New DataTable
        dt = L_prAlumnoAyudaColor(tbSuc.Value, numiInst, tbFechaSelect.Value.ToString("yyyy/MM/dd")) 'gi_userSuc

        For i = 0 To dt.Rows.Count - 1
            dt.Rows(i).Item("color") = _listColores.Item(i).ToArgb()
        Next

        'aumento el datatable de alumnos pero para el siguiente mes
        Dim dt2 As New DataTable
        dt2 = L_prAlumnoAyudaColor(tbSuc.Value, numiInst, DateAdd(DateInterval.Month, 1, tbFechaSelect.Value).ToString("yyyy/MM/dd")) 'gi_userSuc
        For i = 0 To dt2.Rows.Count - 1
            dt2.Rows(i).Item("color") = _listColores.Item(i).ToArgb()
        Next

        grAlumnos.DataSource = dt
        grAlumnos.RetrieveStructure()

        'aumento el datatable de alumnos pero para el siguiente mes
        grAlumnos.Tag = dt2

        'dar formato a las columnas
        With grAlumnos.RootTable.Columns("egnumi")
            .Visible = False
        End With
        With grAlumnos.RootTable.Columns("cbnumi")
            .Caption = "Codigo"
            .Visible = True
            .Width = 70
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With

        With grAlumnos.RootTable.Columns("cbci")
            .Caption = "CI"
            .Visible = False
            .Width = 70
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With

        With grAlumnos.RootTable.Columns("cbnom2")
            .Caption = "Nombre"
            .Width = 250
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
        End With


        With grAlumnos.RootTable.Columns("color")
            .Caption = "Color"
            .Width = 30
        End With

        With grAlumnos.RootTable.Columns("clasesFalt")
            .Caption = "Falta"
            .Width = 30
            .HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With

        With grAlumnos.RootTable.Columns("egest")
            .Visible = False
        End With

        'Habilitar Filtradores
        With grAlumnos
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False

            'diseño de la grilla
            grAlumnos.VisualStyle = VisualStyle.Office2007
        End With
        grAlumnos.SelectionMode = SelectionMode.SingleSelection
        grAlumnos.ContextMenuStrip = msOpsAlumnos

        'cargar condicion
        Dim fc, fc1 As GridEXFormatCondition
        fc = New GridEXFormatCondition(grAlumnos.RootTable.Columns("clasesFalt"), ConditionOperator.GreaterThan, 0)
        fc.FormatStyle.ForeColor = Color.Red
        grAlumnos.RootTable.FormatConditions.Add(fc)

        fc1 = New GridEXFormatCondition(grAlumnos.RootTable.Columns("egest"), ConditionOperator.Equal, 2)
        fc1.FormatStyle.BackColor = Color.LightCyan
        grAlumnos.RootTable.FormatConditions.Add(fc1)

        'intengar poner borde a la grilla


    End Sub

    Private Function _prGetColorFromHex(hexcolor As String) As Color
        'hex = Replace(hex, "#", "")
        'hex = "&H" & hex
        'ColorTranslator.FromOle(hex)
        Dim Red As String
        Dim Green As String
        Dim Blue As String
        hexcolor = Replace(hexcolor, "#", "")
        Red = Val("&H" & Mid(hexcolor, 1, 2))
        Green = Val("&H" & Mid(hexcolor, 3, 2))
        Blue = Val("&H" & Mid(hexcolor, 5, 2))
        Return Color.FromArgb(Red, Green, Blue)
    End Function

    Private Sub _prRepaintGrilla(mes As Integer)
        If mes = 1 Then
            For f As Integer = 0 To _meses.vmes1.vHoras.Count - 1
                For c As Integer = 0 To _meses.vmes1.vUltDia - 1
                    If IsNothing(_meses.vmes1.vDias(f, c)) = False Then
                        grHorario.Rows(f).Cells(c + 1).Style.BackColor = _meses.vmes1.vDias(f, c).ccolor
                        If _meses.vmes1.vDias(f, c).desc <> String.Empty Then
                            grHorario.Rows(f).Cells(c + 1).Style.Font = New Font("Arial", 9, FontStyle.Bold) 'Or FontStyle.Underline
                        End If

                        If _meses.vmes1.vDias(f, c).estado = conDiaGrabadoAlumno Or _meses.vmes1.vDias(f, c).estado = conDiaMarcado Then
                            If _meses.vmes1.vDias(f, c).estadoCls = conEstClasePermiso Then
                                grHorario.Rows(f).Cells(c + 1).Value = "P"
                            Else
                                If _meses.vmes1.vDias(f, c).estadoCls = conEstClaseSuspension Then
                                    grHorario.Rows(f).Cells(c + 1).Value = "S"
                                Else
                                    Dim numiCab As Integer = _meses.vmes1.vDias(f, c).numCla
                                    Dim numiAlm As Integer = _meses.vmes1.vDias(f, c).numiAlm
                                    Dim estAlm As Integer = CInt(CType(grAlumnos.DataSource, DataTable).Select("cbnumi=" + numiAlm.ToString)(0).Item("egest"))
                                    If (estAlm = 2) Then
                                        grHorario.Rows(f).Cells(c + 1).Value = "R" + _meses.vmes1.vDias(f, c).numCla.ToString
                                    ElseIf (_meses.vmes1.vDias(f, c).estado = 1) Then
                                        grHorario.Rows(f).Cells(c + 1).Value = _meses.vmes1.vDias(f, c).numCla.ToString
                                    End If
                                End If
                            End If

                            If _meses.vmes1.vDias(f, c).estadoCls = conEstClaseAsistida Then
                                grHorario.Rows(f).Cells(c + 1).Style.ForeColor = Color.Green
                            End If
                        End If

                        'verifico si es un dia
                        If _meses.vmes1.vDias(f, c).claseSobrepuesta = True Then
                            'grHorario.Rows(f).Cells(c + 1).Style.BackColor = Color.Yellow
                            grHorario.Rows(f).Cells(c + 1).Style.Font = New Font("Arial", 9, FontStyle.Bold Or FontStyle.Underline)
                        End If

                        'verifico si es una hora liberada
                        If _meses.vmes1.vDias(f, c).estado = conHoraLiberada Then
                            grHorario.Rows(f).Cells(c + 1).Value = "L"
                            grHorario.Rows(f).Cells(c + 1).Style.ForeColor = Color.White

                        End If

                        'verifico si es una hora Diligencia
                        If _meses.vmes1.vDias(f, c).estado = conHoraDiligencia Then
                            grHorario.Rows(f).Cells(c + 1).Value = "D"
                            grHorario.Rows(f).Cells(c + 1).Style.ForeColor = Color.White

                        End If
                    End If
                Next
            Next
        Else
            For f As Integer = 0 To _meses.vmes2.vHoras.Count - 1
                For c As Integer = 0 To _meses.vmes2.vUltDia - 1
                    If IsNothing(_meses.vmes2.vDias(f, c)) = False Then
                        grHorario.Rows(f).Cells(c + 1).Style.BackColor = _meses.vmes2.vDias(f, c).ccolor
                        If _meses.vmes2.vDias(f, c).desc <> String.Empty Then
                            grHorario.Rows(f).Cells(c + 1).Style.Font = New Font("Arial", 10, FontStyle.Bold)
                        End If
                        If _meses.vmes2.vDias(f, c).estado = conDiaGrabadoAlumno Or _meses.vmes2.vDias(f, c).estado = conDiaMarcado Then
                            If _meses.vmes2.vDias(f, c).estadoCls = conEstClasePermiso Then
                                grHorario.Rows(f).Cells(c + 1).Value = "P"
                            Else
                                If _meses.vmes2.vDias(f, c).estadoCls = conEstClaseSuspension Then
                                    grHorario.Rows(f).Cells(c + 1).Value = "S"
                                Else
                                    Dim numiCab As Integer = _meses.vmes2.vDias(f, c).numCla
                                    Dim numiAlm As Integer = _meses.vmes2.vDias(f, c).numiAlm
                                    Dim filaFiltrada As DataRow() = CType(grAlumnos.DataSource, DataTable).Select("cbnumi=" + numiAlm.ToString)
                                    If filaFiltrada.Count > 0 Then
                                        Dim estAlm As Integer = CInt(filaFiltrada(0).Item("egest"))
                                        If (estAlm = 2) Then
                                            grHorario.Rows(f).Cells(c + 1).Value = "R" + _meses.vmes2.vDias(f, c).numCla.ToString
                                        ElseIf (_meses.vmes2.vDias(f, c).estado = 1) Then
                                            grHorario.Rows(f).Cells(c + 1).Value = _meses.vmes2.vDias(f, c).numCla.ToString
                                        End If
                                    End If

                                End If
                            End If

                        End If

                        If _meses.vmes2.vDias(f, c).estadoCls = 1 Then 'ojo aca deberia de ser estado=0
                            grHorario.Rows(f).Cells(c + 1).Style.ForeColor = Color.Green
                        End If

                        'verifico si es un dia
                        If _meses.vmes2.vDias(f, c).claseSobrepuesta = True Then
                            'grHorario.Rows(f).Cells(c + 1).Style.BackColor = Color.Yellow
                            grHorario.Rows(f).Cells(c + 1).Style.Font = New Font("Arial", 10, FontStyle.Bold Or FontStyle.Underline)
                        End If

                        'verifico si es una hora liberada
                        If _meses.vmes2.vDias(f, c).estado = conHoraLiberada Then
                            grHorario.Rows(f).Cells(c + 1).Value = "L"
                            grHorario.Rows(f).Cells(c + 1).Style.ForeColor = Color.White
                        End If

                        'verifico si es una hora diligencia
                        If _meses.vmes2.vDias(f, c).estado = conHoraDiligencia Then
                            grHorario.Rows(f).Cells(c + 1).Value = "D"
                            grHorario.Rows(f).Cells(c + 1).Style.ForeColor = Color.White
                        End If
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub _prCargarGridHorario(fecha As DateTime, numiInst As String)
        'para corregir el problema de fechas,donde no me muestra bien
        fecha = New Date(fecha.Year, fecha.Month, 1)

        _mesVisto = 1
        _marcando = False

        _dt = New DataTable()
        _dt = L_prClasesPracDetFechasEsctructuraGeneral()
        grHorario.DataSource = _dt

        Dim dtCabecera As DataTable = _dt.Copy 'L_prClasesPracDetFechasEsctructuraGeneral()
        grCabecera.DataSource = dtCabecera

        'CARGAR LA ESTRUCTURA DE LOS MESES
        '_meses = New ClsMeses(fecha, numiInst, CType(grAlumnos.DataSource, DataTable), tbSuc.Value)
        _meses = New ClsMeses(fecha, numiInst, CType(grAlumnos.DataSource, DataTable), CType(grAlumnos.Tag, DataTable), tbSuc.Value)

        Dim fuente As New Font("Tahoma", 9, FontStyle.Regular)
        For i = 1 To 31
            Dim col As String = "d" + Str(i).Trim
            With grHorario.Columns(col) '"d" + Str(i)
                fecha = fecha.AddDays(-1)
                .HeaderText = Str(i) '+ "(" + WeekdayName(Weekday(fecha), True, FirstDayOfWeek.Monday) + ")"
                grCabecera.Columns(col).HeaderText = WeekdayName(Weekday(fecha), True, FirstDayOfWeek.Monday)
                fecha = fecha.AddDays(1)
                .HeaderCell.Style.Font = fuente
                .Width = 25
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                fecha = fecha.AddDays(1)
                .Visible = True
                .DefaultCellStyle.Font = fuente

                grCabecera.Columns(col).HeaderCell.Style.Font = fuente
                grCabecera.Columns(col).Width = 25
                grCabecera.Columns(col).Visible = True
                If i > _meses.vmes1.vUltDia Then
                    .Visible = False
                    grCabecera.Columns(col).Visible = False
                End If

            End With
        Next


        'aumentar los horarios
        With grHorario.Columns(0) '"d" + Str(i)
            grCabecera.Columns(0).HeaderCell.Style.Font = fuente
            grCabecera.Columns(0).Width = 50
            grCabecera.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grCabecera.Columns(0).DefaultCellStyle.BackColor = Color.Azure

            .HeaderText = "HORA"
            .HeaderCell.Style.Font = fuente
            .Width = 50
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.BackColor = Color.Azure
        End With

        _dt.Rows.Clear()
        For I = 0 To _meses.vmes1.vHoras.Count - 1
            _dt.Rows.Add(_meses.vmes1.vHoras.Item(I))
        Next

        grHorario.AllowUserToAddRows = False
        grHorario.Refresh()

        'pongo el menu
        grHorario.ContextMenuStrip = msOpsHorario
        grHorario.AllowDrop = False
        grHorario.AllowUserToOrderColumns = False
        For Each col As DataGridViewColumn In grHorario.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        'cargar los datos del mes
        _prRepaintGrilla(1)

    End Sub

    Private Sub _prCargarGridHorarioSinReestructurar(mesSelect As Integer)
        _dt = New DataTable()
        _dt = L_prClasesPracDetFechasEsctructuraGeneral()
        grHorario.DataSource = _dt

        Dim dtCabecera As DataTable = L_prClasesPracDetFechasEsctructuraGeneral()
        grCabecera.DataSource = dtCabecera

        'CARGAR LA ESTRUCTURA DE LOS MESES
        '_meses = New ClsMeses(fecha, numiInst, CType(grAlumnos.DataSource, DataTable))
        Dim fecha As Date
        Dim ultDia As Integer
        If mesSelect = 1 Then
            fecha = _meses.vmes1.vFecha
            ultDia = _meses.vmes1.vUltDia
        Else
            fecha = _meses.vmes2.vFecha
            ultDia = _meses.vmes2.vUltDia
        End If

        Dim fuente As New Font("Tahoma", 10, FontStyle.Regular)
        For i = 1 To 31
            Dim col As String = "d" + Str(i).Trim
            With grHorario.Columns(col) '"d" + Str(i)
                fecha = fecha.AddDays(-1)
                .HeaderText = Str(i) '+ "(" + WeekdayName(Weekday(fecha), True, FirstDayOfWeek.Monday) + ")"
                grCabecera.Columns(col).HeaderText = WeekdayName(Weekday(fecha), True, FirstDayOfWeek.Monday)
                fecha = fecha.AddDays(1)
                .HeaderCell.Style.Font = fuente
                .Width = 25
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                fecha = fecha.AddDays(1)
                .Visible = True

                grCabecera.Columns(col).HeaderCell.Style.Font = fuente
                grCabecera.Columns(col).Width = 25
                grCabecera.Columns(col).Visible = True


                If i > ultDia Then
                    .Visible = False
                    grCabecera.Columns(col).Visible = False
                End If

            End With
        Next


        'aumentar los horarios
        With grHorario.Columns(0) '"d" + Str(i)
            grCabecera.Columns(0).HeaderCell.Style.Font = fuente
            grCabecera.Columns(0).Width = 50
            grCabecera.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grCabecera.Columns(0).DefaultCellStyle.BackColor = Color.Azure

            .HeaderText = "HORA"
            .HeaderCell.Style.Font = fuente
            .Width = 50
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.BackColor = Color.Azure
        End With

        _dt.Rows.Clear()
        If mesSelect = 1 Then
            For i = 0 To _meses.vmes1.vHoras.Count - 1
                _dt.Rows.Add(_meses.vmes1.vHoras.Item(i))
            Next
        Else
            For i = 0 To _meses.vmes2.vHoras.Count - 1
                _dt.Rows.Add(_meses.vmes2.vHoras.Item(i))
            Next
        End If


        grHorario.AllowUserToAddRows = False
        grHorario.Refresh()

        'pongo el menu
        If btnEliminar.Visible Then
            grHorario.ContextMenuStrip = msOpsVerDisp
        Else
            grHorario.ContextMenuStrip = msOpsHorario
        End If

        For Each col As DataGridViewColumn In grHorario.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next


        'cargar los datos del mes
        _prRepaintGrilla(mesSelect)

    End Sub

    Private Sub _prCargarGridHorarioSinGrabaciones(fecha As DateTime)
        _mesVisto = 1
        _marcando = False

        _dt = New DataTable()
        _dt = L_prClasesPracDetFechasEsctructuraGeneral()
        grHorario.DataSource = _dt

        Dim dtCabecera As DataTable = L_prClasesPracDetFechasEsctructuraGeneral()
        grCabecera.DataSource = dtCabecera

        'CARGAR LA ESTRUCTURA DE LOS MESES
        _meses = New ClsMeses(fecha, tbSuc.Value)


        Dim fuente As New Font("Tahoma", 10, FontStyle.Regular)
        For i = 1 To 31
            Dim col As String = "d" + Str(i).Trim
            With grHorario.Columns(col) '"d" + Str(i)
                fecha = fecha.AddDays(-1)
                .HeaderText = Str(i) '+ "(" + WeekdayName(Weekday(fecha), True, FirstDayOfWeek.Monday) + ")"
                grCabecera.Columns(col).HeaderText = WeekdayName(Weekday(fecha), True, FirstDayOfWeek.Monday)
                fecha = fecha.AddDays(1)
                .HeaderCell.Style.Font = fuente
                .Width = 25
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                fecha = fecha.AddDays(1)
                .Visible = True

                grCabecera.Columns(col).HeaderCell.Style.Font = fuente
                grCabecera.Columns(col).Width = 25
                grCabecera.Columns(col).Visible = True
                If i > _meses.vmes1.vUltDia Then
                    .Visible = False
                    grCabecera.Columns(col).Visible = False
                End If

            End With
        Next


        'aumentar los horarios
        With grHorario.Columns(0) '"d" + Str(i)
            grCabecera.Columns(0).HeaderCell.Style.Font = fuente
            grCabecera.Columns(0).Width = 50
            grCabecera.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grCabecera.Columns(0).DefaultCellStyle.BackColor = Color.Azure

            .HeaderText = "HORA"
            .HeaderCell.Style.Font = fuente
            .Width = 50
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.BackColor = Color.Azure
        End With

        _dt.Rows.Clear()
        For I = 0 To _meses.vmes1.vHoras.Count - 1
            _dt.Rows.Add(_meses.vmes1.vHoras.Item(I))
        Next

        grHorario.AllowUserToAddRows = False
        grHorario.Refresh()

        'pongo el menu
        grHorario.ContextMenuStrip = msOpsVerDisp

        'cargar los datos del mes
        _prRepaintGrilla(1)

    End Sub

    'Private Sub _prCargarGridHorario1(fecha As DateTime, numiInst As String)
    '    _dt = New DataTable()
    '    _dt = L_prClasesPracDetFechasEsctructuraGeneral()
    '    grHorario.DataSource = _dt

    '    Dim fuente As New Font("Tahoma", 10, FontStyle.Regular)
    '    For i = 1 To 31
    '        Dim col As String = "d" + Str(i).Trim
    '        With grHorario.Columns(col) '"d" + Str(i)
    '            If fecha.DayOfWeek = DayOfWeek.Sunday Then
    '                .DefaultCellStyle.BackColor = Color.Red
    '            Else
    '                .DefaultCellStyle.BackColor = Color.White
    '            End If
    '            fecha = fecha.AddDays(-1)
    '            .HeaderText = Str(i) + "(" + WeekdayName(Weekday(fecha), True, FirstDayOfWeek.Monday) + ")"
    '            fecha = fecha.AddDays(1)
    '            .HeaderCell.Style.Font = fuente
    '            .Width = 25
    '            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '            fecha = fecha.AddDays(1)
    '        End With
    '    Next

    '    'aumentar los horarios
    '    With grHorario.Columns(0) '"d" + Str(i)
    '        .HeaderText = "HORA"
    '        .HeaderCell.Style.Font = fuente
    '        .Width = 50
    '        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '        .DefaultCellStyle.BackColor = Color.Azure
    '    End With


    '    Dim hora As DateTime = New DateTime(2016, 10, 1, 7, 0, 0)
    '    _dt.Rows(0).Item("hora") = hora.Hour.ToString("00") + ":" + hora.Minute.ToString("00")
    '    For I = 0 To 12
    '        If I <= 5 Then
    '            hora = hora.AddMinutes(50)
    '        Else
    '            hora = hora.AddHours(1)
    '        End If
    '        _dt.Rows.Add(hora.Hour.ToString("00") + ":" + hora.Minute.ToString("00"))
    '    Next

    '    grHorario.AllowUserToAddRows = False

    '    'cargar los datos de la grilla
    '    Dim numiAlum As String
    '    For i = 0 To grAlumnos.RowCount - 1
    '        grAlumnos.Row = i
    '        numiAlum = grAlumnos.GetValue("cbnumi")
    '        _prCargarDatosDeFecha(numiInst, numiAlum)
    '    Next
    '    _prCargarFeriados(numiInst)
    '    grHorario.Refresh()

    '    'pongo el menu
    '    grHorario.ContextMenuStrip = msOpsHorario
    'End Sub


    'Private Sub _prCargarDatosDeFecha(numiInstruc As String, numiAlum As String)
    '    Dim dtFechas As DataTable = L_prClasesPracDetFechasPorAlumnoGeneral(numiInstruc, numiAlum)
    '    Dim num As Integer = 1
    '    For Each fila As DataRow In dtFechas.Rows
    '        Dim fecha As Date = fila.Item("ehfec")
    '        Dim hora As String = fila.Item("ehhor")
    '        Dim i As Integer
    '        For i = 0 To _dt.Rows.Count - 1
    '            Dim hora2 As String = _dt.Rows(i).Item("hora")
    '            If hora = hora2 Then
    '                Exit For
    '            End If
    '        Next
    '        If fecha.Month = tbFechaSelect.Value.Month Then
    '            Select Case grAlumnos.Row
    '                Case 0
    '                    grHorario.Rows(i).Cells(fecha.Day).Style.BackColor = Color.Blue
    '                Case 1
    '                    grHorario.Rows(i).Cells(fecha.Day).Style.BackColor = Color.Orange
    '                Case 2
    '                    grHorario.Rows(i).Cells(fecha.Day).Style.BackColor = Color.Violet
    '                Case 3
    '                    grHorario.Rows(i).Cells(fecha.Day).Style.BackColor = Color.Beige
    '                Case 4
    '                    grHorario.Rows(i).Cells(fecha.Day).Style.BackColor = Color.Gold
    '                Case 5
    '                    grHorario.Rows(i).Cells(fecha.Day).Style.BackColor = Color.LightGreen
    '            End Select
    '            grHorario.Rows(i).Cells(fecha.Day).Value = num
    '            num = num + 1
    '        End If
    '    Next
    'End Sub
    'Private Sub _prCargarFeriados(numiInstruc As String)
    '    Dim dtFeriados As DataTable = L_prFeriadoGeneralPorFecha(tbFechaSelect.Value.ToString("yyyy-MM-dd"))
    '    For Each fila As DataRow In dtFeriados.Rows
    '        Dim fFeriado As Date = fila.Item("pfflib")
    '        Dim desc As String = fila.Item("pfdes")

    '        Dim col As String = "d" + fFeriado.Day.ToString
    '        With grHorario.Columns(col)
    '            .DefaultCellStyle.BackColor = Color.Yellow
    '            For Each f In _dt.Rows 'recorro todas las columnas para poner que es feriado
    '                f.item(col) = desc
    '            Next
    '        End With
    '    Next
    'End Sub

    Private Sub _prGrabarRegistro()
        If _numiAlumInscrito = -1 Then
            'grabar horario de forma normal como si estuvieran de forma nativa en el programa
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable = L_prAlumnoLibresInstructorAyuda(tbSuc.Value, tbPersona.Value) 'gi_userSuc
            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("cbnumi", True, "Codigo".ToUpper, 70))
            listEstCeldas.Add(New Modelos.Celda("cbci", True, "CI".ToUpper, 70))
            listEstCeldas.Add(New Modelos.Celda("cbnom2", True, "Nombre completo".ToUpper, 300))

            frmAyuda = New Modelos.ModeloAyuda(600, 300, dt, "Seleccione el estudiante a quien se programara el horario".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                Dim numiAalumno As String = frmAyuda.filaSelect.Cells("cbnumi").Value
                Dim numiReg As String = ""
                Dim respuesta As Boolean = L_prClasesPracCabeceraDetalleGrabar2(numiReg, tbPersona.Value, numiAalumno, IIf(_isClasePractica = True, 1, 2), _cantClasesPracticas, _cantClasesReforzamiento, _dtFechas)
                If respuesta Then
                    _prCargarGridAlumnos(tbPersona.Value)
                    _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                    btnNuevo.Enabled = False
                    _dtFechas.Rows.Clear()
                    tbFechaSelect.Enabled = True
                    '_limiteDias = 15
                    _limiteDias = _cantClasesPracticas
                    _marcarManual = 0
                End If
            End If
        Else
            'grabar un alumno recien inscrito
            Dim numiAalumno As String = _numiAlumInscrito
            Dim numiReg As String = ""
            Dim respuesta As Boolean = L_prClasesPracCabeceraDetalleGrabar2(numiReg, tbPersona.Value, numiAalumno, IIf(_isClasePractica = True, 1, 2), _cantClasesPracticas, _cantClasesReforzamiento, _dtFechas)
            If respuesta Then
                _prCargarGridAlumnos(tbPersona.Value)
                _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                btnNuevo.Enabled = False
                _dtFechas.Rows.Clear()
                tbFechaSelect.Enabled = True
                '_limiteDias = 15
                _limiteDias = _cantClasesPracticas
                _numiAlumInscrito = -1
                _marcarManual = 0
            End If
        End If
    End Sub

    Private Sub _prSetHorarioCorrido()
        '_prCargarGridAlumnos(tbPersona.Value)
        '_prCargarGridHorario(DateTimePicker1.Value, tbPersona.Value)
        If grHorario.CurrentCell.ColumnIndex < 1 Then
            Return
        End If

        If btnNuevo.Enabled = True Then
            ToastNotification.Show(Me, "si quiere volver a marcar, primero limpie el horario con el boton 'limpiar' ".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            Exit Sub
        End If

        Dim i As Integer = 1
        Dim c As Integer = grHorario.CurrentCell.ColumnIndex
        Dim f As Integer = grHorario.CurrentCell.RowIndex

        _dtFechas.Rows.Clear()
        Dim vistaFecha As Integer = 1
        Dim c1 As Integer = 1
        Dim fecha1 As Date = tbFechaSelect.Value
        'Dim fecha2 As Date = DateAdd(DateInterval.Month, 1, tbFechaSelect.Value)
        While i <= _cantClasesPracticas
            If c - 1 <= _meses.vmes1.vUltDia - 1 And vistaFecha = 1 Then
                If IsNothing(_meses.vmes1.vDias(f, c - 1)) Then
                    grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
                    grHorario.CurrentRow.Cells(c).Value = i
                    _dtFechas.Rows.Add(1, 1, 1, New Date(fecha1.Year, fecha1.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                    'cargar dia marcado
                    _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, i, 0)
                    i = i + 1
                Else
                    If _meses.vmes1.vDias(f, c - 1).estado = conDiaGrabadoAlumno And (_meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseAsistida Or _meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseProgramada) Then
                        Dim diaConflicto As String = c.ToString + "/" + _meses.vmes1.vFecha.Month.ToString + "/" + _meses.vmes1.vFecha.Year.ToString
                        Dim info As New TaskDialogInfo("clases practicas".ToUpper, eTaskDialogIcon.Delete, "conflicto con otro horario el dia ".ToUpper + diaConflicto,
                                                       "solo se pudo programar ".ToUpper + (i - 1).ToString + " clases por que hay conflicto con otro horario el dia ".ToUpper + diaConflicto, eTaskDialogButton.Ok Or eTaskDialogButton.Close, eTaskDialogBackgroundColor.Blue)
                        Dim result As eTaskDialogResult = TaskDialog.Show(info)
                        If result = eTaskDialogResult.Yes Then

                        End If
                        Exit While
                    Else
                        If _meses.vmes1.vDias(f, c - 1).estado = conDiaGrabadoAlumno And (_meses.vmes1.vDias(f, c - 1).estadoCls = conEstClasePermiso Or _meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseSuspension) Then
                            grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
                            grHorario.CurrentRow.Cells(c).Value = i
                            _dtFechas.Rows.Add(1, 1, 1, New Date(fecha1.Year, fecha1.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                            'cargar dia marcado
                            _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, i, 0)
                            i = i + 1
                        End If

                    End If

                End If
            Else
                vistaFecha = 2
                If IsNothing(_meses.vmes2.vDias(f, c1 - 1)) Then
                    _meses._prCargarDiaMes2(f, c1 - 1, Color.Green, -1, i, 0)
                    _dtFechas.Rows.Add(1, 1, 1, New Date(_meses.vmes2.vFecha.Year, _meses.vmes2.vFecha.Month, c1).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                    i = i + 1
                Else
                    If _meses.vmes2.vDias(f, c1 - 1).estado = conDiaGrabadoAlumno And (_meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseAsistida Or _meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseProgramada) Then
                        Dim diaConflicto As String = c1.ToString + "/" + _meses.vmes2.vFecha.Month.ToString + "/" + _meses.vmes1.vFecha.Year.ToString
                        Dim info As New TaskDialogInfo("clases practicas".ToUpper, eTaskDialogIcon.Delete, "conflicto con otro horario el dia ".ToUpper + diaConflicto,
                                                       "solo se pudo programar ".ToUpper + (i - 1).ToString + " clases por que hay conflicto con otro horario el dia ".ToUpper + diaConflicto, eTaskDialogButton.Ok Or eTaskDialogButton.Close, eTaskDialogBackgroundColor.Blue)
                        Dim result As eTaskDialogResult = TaskDialog.Show(info)
                        If result = eTaskDialogResult.Yes Then

                        End If
                        Exit While
                    Else
                        If _meses.vmes2.vDias(f, c1 - 1).estado = conDiaGrabadoAlumno And (_meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClasePermiso Or _meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseSuspension) Then
                            _meses._prCargarDiaMes2(f, c1 - 1, Color.Green, -1, i, 0)
                            _dtFechas.Rows.Add(1, 1, 1, New Date(_meses.vmes2.vFecha.Year, _meses.vmes2.vFecha.Month, c1).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                            i = i + 1
                        End If

                    End If
                End If
            End If

            If vistaFecha = 1 Then
                c = c + 1
            Else
                c1 = c1 + 1
            End If
        End While

        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        _marcando = True
        _marcarManual = 0
        _i = 1
        tbFechaSelect.Enabled = False

        _isClasePractica = True
    End Sub

    Private Sub _prSetHorarioCorridoReforzamiento()
        '_prCargarGridAlumnos(tbPersona.Value)
        '_prCargarGridHorario(DateTimePicker1.Value, tbPersona.Value)

        If btnNuevo.Enabled = True Then
            ToastNotification.Show(Me, "si quiere volver a marcar, primero limpie el horario con el boton 'limpiar' ".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            Exit Sub
        End If

        Dim i As Integer = 1
        Dim c As Integer = grHorario.CurrentCell.ColumnIndex
        Dim f As Integer = grHorario.CurrentCell.RowIndex

        _dtFechas.Rows.Clear()
        Dim vistaFecha As Integer = 1
        Dim c1 As Integer = 1
        While i <= _cantClasesReforzamiento
            If c - 1 <= _meses.vmes1.vUltDia - 1 And vistaFecha = 1 Then
                If IsNothing(_meses.vmes1.vDias(f, c - 1)) Then
                    grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
                    grHorario.CurrentRow.Cells(c).Value = i
                    _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                    'cargar dia marcado
                    _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, i, 0)
                    i = i + 1
                Else
                    If _meses.vmes1.vDias(f, c - 1).estado = conDiaGrabadoAlumno And (_meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseAsistida Or _meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseProgramada) Then
                        Dim diaConflicto As String = c.ToString + "/" + _meses.vmes1.vFecha.Month.ToString + "/" + _meses.vmes1.vFecha.Year.ToString
                        Dim info As New TaskDialogInfo("clases practicas".ToUpper, eTaskDialogIcon.Delete, "conflicto con otro horario el dia ".ToUpper + diaConflicto,
                                                       "solo se pudo programar ".ToUpper + (i - 1).ToString + " clases por que hay conflicto con otro horario el dia ".ToUpper + diaConflicto, eTaskDialogButton.Ok Or eTaskDialogButton.Close, eTaskDialogBackgroundColor.Blue)
                        Dim result As eTaskDialogResult = TaskDialog.Show(info)
                        If result = eTaskDialogResult.Yes Then

                        End If
                        Exit While
                    Else
                        If _meses.vmes1.vDias(f, c - 1).estado = conDiaGrabadoAlumno And (_meses.vmes1.vDias(f, c - 1).estadoCls = conEstClasePermiso Or _meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseSuspension) Then
                            grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
                            grHorario.CurrentRow.Cells(c).Value = i
                            _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                            'cargar dia marcado
                            _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, i, 0)
                            i = i + 1
                        End If

                    End If

                End If
            Else
                vistaFecha = 2
                If IsNothing(_meses.vmes2.vDias(f, c1 - 1)) Then
                    _meses._prCargarDiaMes2(f, c1 - 1, Color.Green, -1, i, 0)
                    _dtFechas.Rows.Add(1, 1, 1, New Date(_meses.vmes2.vFecha.Year, _meses.vmes2.vFecha.Month, c1).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                    i = i + 1
                Else
                    If _meses.vmes2.vDias(f, c1 - 1).estado = conDiaGrabadoAlumno And (_meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseAsistida Or _meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseProgramada) Then
                        Dim diaConflicto As String = c1.ToString + "/" + _meses.vmes2.vFecha.Month.ToString + "/" + _meses.vmes1.vFecha.Year.ToString
                        Dim info As New TaskDialogInfo("clases practicas".ToUpper, eTaskDialogIcon.Delete, "conflicto con otro horario el dia ".ToUpper + diaConflicto,
                                                       "solo se pudo programar ".ToUpper + (i - 1).ToString + " clases por que hay conflicto con otro horario el dia ".ToUpper + diaConflicto, eTaskDialogButton.Ok Or eTaskDialogButton.Close, eTaskDialogBackgroundColor.Blue)
                        Dim result As eTaskDialogResult = TaskDialog.Show(info)
                        If result = eTaskDialogResult.Yes Then

                        End If
                        Exit While
                    Else
                        If _meses.vmes2.vDias(f, c1 - 1).estado = conDiaGrabadoAlumno And (_meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClasePermiso Or _meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseSuspension) Then
                            _meses._prCargarDiaMes2(f, c1 - 1, Color.Green, -1, i, 0)
                            _dtFechas.Rows.Add(1, 1, 1, New Date(_meses.vmes2.vFecha.Year, _meses.vmes2.vFecha.Month, c1).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                            i = i + 1
                        End If

                    End If
                End If
            End If

            If vistaFecha = 1 Then
                c = c + 1
            Else
                c1 = c1 + 1
            End If
        End While

        btnNuevo.Enabled = True
        btnModificar.Enabled = True
        _marcando = True
        _marcarManual = 0
        _i = 1
        tbFechaSelect.Enabled = False

        _isClasePractica = False
    End Sub

    Private Sub _prModificarHorarioCorrido()

        If btnNuevo.Enabled = True Then
            ToastNotification.Show(Me, "si quiere volver a marcar, primero limpie el horario con el boton 'limpiar' ".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            Exit Sub
        End If

        Dim i As Integer = 1
        Dim c As Integer = grHorario.CurrentCell.ColumnIndex
        Dim f As Integer = grHorario.CurrentCell.RowIndex

        _dtFechas.Rows.Clear()
        Dim vistaFecha As Integer = 1
        Dim c1 As Integer = 1
        Dim numiAlm As Integer = CType(grAlumnos.DataSource, DataTable).Rows(_AlumSelect).Item("cbnumi")
        While i <= _cantClasesPracticas
            If c - 1 <= _meses.vmes1.vUltDia - 1 And vistaFecha = 1 Then
                If IsNothing(_meses.vmes1.vDias(f, c - 1)) Then
                    grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
                    grHorario.CurrentRow.Cells(c).Value = i
                    _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                    'cargar dia marcado
                    _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, i, 0)
                    i = i + 1
                Else

                    If _meses.vmes1.vDias(f, c - 1).numiAlm = numiAlm Or (_meses.vmes1.vDias(f, c - 1).estado = conDiaGrabadoAlumno And (_meses.vmes1.vDias(f, c - 1).estadoCls = conEstClasePermiso Or _meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseSuspension)) Then
                        grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
                        grHorario.CurrentRow.Cells(c).Value = i
                        _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                        'cargar dia marcado
                        _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, i, 0)
                        i = i + 1
                    Else
                        If _meses.vmes1.vDias(f, c - 1).estado = conDiaGrabadoAlumno And (_meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseAsistida Or _meses.vmes1.vDias(f, c - 1).estadoCls = conEstClaseProgramada) Then
                            Dim diaConflicto As String = c.ToString + "/" + _meses.vmes1.vFecha.Month.ToString + "/" + _meses.vmes1.vFecha.Year.ToString
                            Dim info As New TaskDialogInfo("clases practicas".ToUpper, eTaskDialogIcon.Delete, "conflicto con otro horario el dia ".ToUpper + diaConflicto,
                                                           "solo se pudo programar ".ToUpper + (i - 1).ToString + " clases por que hay conflicto con otro horario el dia ".ToUpper + diaConflicto, eTaskDialogButton.Ok Or eTaskDialogButton.Close, eTaskDialogBackgroundColor.Blue)
                            Dim result As eTaskDialogResult = TaskDialog.Show(info)
                            If result = eTaskDialogResult.Yes Then

                            End If
                            Exit While
                        End If
                    End If

                End If
            Else
                vistaFecha = 2
                If IsNothing(_meses.vmes2.vDias(f, c1 - 1)) Then
                    _meses._prCargarDiaMes2(f, c1 - 1, Color.Green, -1, i, 0)
                    _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, _meses.vmes2.vFecha.Month, c1).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                    i = i + 1
                Else
                    If _meses.vmes2.vDias(f, c1 - 1).numiAlm = numiAlm Or (_meses.vmes2.vDias(f, c1 - 1).estado = conDiaGrabadoAlumno And (_meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClasePermiso Or _meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseSuspension)) Then
                        _meses._prCargarDiaMes2(f, c1 - 1, Color.Green, -1, i, 0)
                        _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, _meses.vmes2.vFecha.Month, c1).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)
                        i = i + 1
                    Else
                        If _meses.vmes2.vDias(f, c1 - 1).estado = conDiaGrabadoAlumno And (_meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseAsistida Or _meses.vmes2.vDias(f, c1 - 1).estadoCls = conEstClaseProgramada) Then
                            Dim diaConflicto As String = c1.ToString + "/" + _meses.vmes2.vFecha.Month.ToString + "/" + _meses.vmes1.vFecha.Year.ToString
                            Dim info As New TaskDialogInfo("clases practicas".ToUpper, eTaskDialogIcon.Delete, "conflicto con otro horario el dia ".ToUpper + diaConflicto,
                                                           "solo se pudo programar ".ToUpper + (i - 1).ToString + " clases por que hay conflicto con otro horario el dia ".ToUpper + diaConflicto, eTaskDialogButton.Ok Or eTaskDialogButton.Close, eTaskDialogBackgroundColor.Blue)
                            Dim result As eTaskDialogResult = TaskDialog.Show(info)
                            If result = eTaskDialogResult.Yes Then

                            End If
                            Exit While
                        End If
                    End If
                End If
            End If

            If vistaFecha = 1 Then
                c = c + 1
            Else
                c1 = c1 + 1
            End If
        End While

        btnNuevo.Enabled = True
        _marcando = True
        _marcarManual = 0
        _i = 1
        tbFechaSelect.Enabled = False
    End Sub

    Private Sub _prSetHorarioManualmente()
        If btnNuevo.Enabled = True Then
            ToastNotification.Show(Me, "si quiere volver a marcar, primero limpie el horario con el boton 'limpiar' ".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            Exit Sub
        End If

        _marcarManual = 1
        btnNuevo.Enabled = True
        _i = 1
        _marcando = True
        tbFechaSelect.Enabled = False
    End Sub

    Private Sub _prModificarRegistro()
        'grabar horario
        Dim numiAalumno As String = CType(grAlumnos.DataSource, DataTable).Rows(_AlumSelect).Item("cbnumi")
        Dim numiReg As String = ""
        Dim respuesta As Boolean = L_prClasesPracCabeceraDetalleGrabarModificando(numiReg, tbPersona.Value, numiAalumno, IIf(_isClasePractica = True, 1, 2), _dtFechas)
        If respuesta Then
            _prCargarGridAlumnos(tbPersona.Value)
            _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
            btnNuevo.Enabled = False
            _dtFechas.Rows.Clear()
            tbFechaSelect.Enabled = True
            '_limiteDias = 15
            _limiteDias = _cantClasesPracticas
        End If

    End Sub

    Private Sub _prAsignarAsistencia()
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)

            If IsNothing(hora) = False Then
                If hora.estado = 1 And hora.estadoCls = conEstClaseProgramada Then
                    Dim line As Integer = _meses.vmes1.vDias(f, c - 1).line

                    Dim obs As String = _meses.vmes1.vDias(f, c - 1).desc
                    Dim estadoClase As String = "1"
                    _dtFechas.Rows.Clear()
                    _dtFechas.Rows.Add(0, 0, line, Nothing, 0, estadoClase, obs, 1)
                    Dim res As Boolean = L_prClasesPracDetalleModificarEstado(_dtFechas)
                    If res Then
                        _dtFechas.Rows.Clear()

                        hora.estadoCls = conEstClaseAsistida

                        'REENUMERAR
                        'Dim numiAlum As String = hora.numiAlm
                        'Dim dtHorasGrabadas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, numiAlum, tbFechaSelect.Value.ToString("yyyy-MM-dd"))
                        'Dim numero As Integer = 1
                        'For Each filaHora As DataRow In dtHorasGrabadas.Rows
                        '    Dim horaFecha As String = filaHora.Item("ehhor")
                        '    Dim fechaHora As Date = filaHora.Item("ehfec")

                        '    Dim f1 As Integer = _meses.vmes1.vHoras.IndexOf(horaFecha)
                        '    Dim hora1 As ClsHora = _meses.vmes1.vDias(f1, fechaHora.Day - 1)
                        '    If hora1.estadoCls = conEstClaseProgramada Or hora1.estadoCls = conEstClaseFalta Or hora1.estadoCls = conEstClaseAsistida Then
                        '        hora1.numCla = numero
                        '        numero = numero + 1
                        '    End If

                        'Next

                        _prRepaintGrilla(1)

                        '_prCargarGridAlumnos(tbPersona.Value)
                        '_prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub _prAsignarTodasLasAsistencias()
        Dim f As Integer
        f = grAlumnos.Row

        If f >= 0 Then
            Dim res As Boolean = L_prClasesPracDetalleModificarEstado2(tbPersona.Value, grAlumnos.GetValue("cbnumi"), "1")
            If res Then
                ToastNotification.Show(Me, "se grabo todas las asistencias del alumno".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _dtFechas.Rows.Clear()
                _prCargarGridAlumnos(tbPersona.Value)
                _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
            Else
                ToastNotification.Show(Me, "no se pudo realizar la operacion".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            End If

        End If


    End Sub

    Private Sub _prSeleccionarHoras()

        Dim dtHoras As DataTable = L_prHoraDetDelMesGeneral(tbFechaSelect.Value.ToString("yyyy/MM/dd"), tbSuc.Value, gi_LibHORARIOTipoPractEscuela)
        Dim vHoras As List(Of String) = New List(Of String)
        For Each fila As DataRow In dtHoras.Rows
            vHoras.Add(fila.Item("cchora"))
        Next

        Dim dtFechas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, grAlumnos.GetValue("cbnumi"), tbFechaSelect.Value.ToString("yyyy/MM/dd"))

        Dim i As Integer = 0
        While i < grHorario.SelectedCells.Count
            grHorario.SelectedCells(i).Selected = False
        End While

        'For i = 0 To grHorario.SelectedCells.Count - 2
        '    grHorario.SelectedCells(i).Selected = False
        'Next

        For Each fila As DataRow In dtFechas.Rows
            Dim fecha As Date = fila.Item("ehfec")
            Dim hora As String = fila.Item("ehhor")
            Dim f As Integer = vHoras.IndexOf(hora)

            grHorario.Rows(f).Cells(fecha.Day).Selected = True
        Next

    End Sub

    Private Sub _prLiberarHoraEmpezar()
        btnImprimir.Text = "Confirmar".ToUpper
        PanelToolBar1.Visible = False
        _marcarManual = 2
    End Sub
    Private Sub _prLiberarHoraGrabar()
        Dim fecha As Date
        Dim hora As String
        Dim dtHoras As DataTable = New DataTable



        Dim obs As String = InputBox("ingrese la observacion".ToUpper, "OBSERVACION".ToUpper, "").ToUpper
        If obs <> String.Empty Then
            For Each fila As DataRow In _dtHorasLiberar.Rows
                fecha = fila.Item("ehhfec") 'CType(fila.Item("ehfec"), Date).ToString("yyyy/MM/dd")
                hora = fila.Item("ehhhor").ToString

                If tbTodosInst.Value = True Then
                    dtHoras = L_prClasesPracDetFechasGetPorFechaHora(fecha.ToString("yyyy/MM/dd"), hora)
                Else
                    dtHoras = L_prClasesPracDetFechasGetPorFechaHoraInstructor(fecha.ToString("yyyy/MM/dd"), hora, tbPersona.Value)
                End If

                For Each fila2 As DataRow In dtHoras.Rows
                    fila2.Item("ehest") = conEstClaseSuspension
                    fila2.Item("ehobs") = "(suspension de escuela) ".ToUpper + fila2.Item("ehobs")
                Next
                L_prClasesPracDetalleModificarEstado(dtHoras)
            Next

            If tbTodosInst.Value = True Then
                L_prHoraLibreTCE0062GrabarTodosInstructoresPorSucursal(_dtHorasLiberar, tbSuc.Value, obs)
            Else
                L_prHoraLibreTCE0062GrabarPorInstructor(_dtHorasLiberar, obs)
            End If
        End If
        'reiniciar todo
        btnImprimir.Text = "liberar hora".ToUpper
        PanelToolBar1.Visible = True

        _marcarManual = 0
        _prCargarGridAlumnos(tbPersona.Value)
        _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
        btnNuevo.Enabled = False
        _dtHorasLiberar.Rows.Clear()
        tbFechaSelect.Enabled = True
        '_limiteDias = 15
        _limiteDias = _cantClasesPracticas
    End Sub

    Private Sub _prInsertarTablaLiberarHora(fecha As Date, hora As String, numiInst As String)
        _dtHorasLiberar.Rows.Add(0, numiInst, fecha, hora, "", 0)
    End Sub

    Private Sub _prInsertarTablaHoraDiligencia(dt As DataTable, fecha As Date, hora As String, numiInst As String)
        dt.Rows.Add(0, numiInst, fecha, hora, "", 0)
    End Sub

    Private Sub _prVerDisponibilidad()
        _prCargarGridHorarioSinGrabaciones(tbFechaSelect.Value)
        grAlumnos.DataSource = Nothing
        tbPersona.Text = ""

        PanelFechas.Enabled = True

        btnEliminar.Visible = True
        btnNuevo.Visible = False
        btnNuevo.Enabled = False
        btnModificar.Visible = False
        _marcando = False

    End Sub
    Private Sub _prLimpiar()
        If tbPersona.SelectedIndex >= 0 Then
            _prCargarGridAlumnos(tbPersona.Value)
            _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
        Else
            grHorario.DataSource = Nothing
            grAlumnos.DataSource = Nothing
        End If

        btnNuevo.Enabled = False
        btnNuevo.Visible = True
        btnModificar.Visible = True
        btnEliminar.Visible = False
        PanelToolBar1.Refresh()

        _marcarManual = 0
        _dtFechas.Rows.Clear()
    End Sub

    Private Sub _prMostrarChoferesDisponibles()
        If _dtFechas.Rows.Count = 0 Then
            ToastNotification.Show(Me, "para confirmar seleccion, primero marque con click derecho sobre el horario".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
            Exit Sub
        End If

        Dim dtInts As DataTable = L_prPersonaAyudaGeneralPorSucursal(tbSuc.Value, gi_LibPERSTIPOInstructor)
        Dim dtInstFinal As DataTable = L_prPersonaAyudaGeneralPorSucursal(-1, gi_LibPERSTIPOInstructor)

        Dim dtClases As DataTable
        Dim fecha As Date
        Dim hora, numiInst As String
        For Each fila As DataRow In dtInts.Rows
            Dim tiene As Boolean = False
            numiInst = fila("panumi")
            For Each fila2 As DataRow In _dtFechas.Rows
                fecha = fila2.Item("ehfec") 'CType(fila.Item("ehfec"), Date).ToString("yyyy/MM/dd")
                hora = fila2.Item("ehhor").ToString
                dtClases = L_prClasesPracDetFechasGetPorFechaHoraInstructor(fecha.ToString("yyyy/MM/dd"), hora, numiInst)
                If dtClases.Rows.Count > 0 Then
                    tiene = True
                    Exit For
                End If
            Next
            If tiene = False Then
                dtInstFinal.Rows.Add(fila.ItemArray)
            End If
        Next

        If dtInstFinal.Rows.Count > 0 Then
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("panumi", True, "Codigo".ToUpper, 70))
            listEstCeldas.Add(New Modelos.Celda("panom", False))
            listEstCeldas.Add(New Modelos.Celda("paape", False))
            listEstCeldas.Add(New Modelos.Celda("panom1", True, "Nombre completo".ToUpper, 300))

            frmAyuda = New Modelos.ModeloAyuda(600, 300, dtInstFinal, "seleccione uno de los instructores que esta libre en el horario selccionado".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()

            If frmAyuda.seleccionado = True Then
                'ahora selecciono esa fecha para hacer la grabacion del horario

                Dim dtHoras As DataTable = L_prHoraDetDelMesGeneral(tbFechaSelect.Value.ToString("yyyy/MM/dd"), tbSuc.Value, gi_LibHORARIOTipoPractEscuela)
                Dim vHoras As List(Of String) = New List(Of String)
                For Each fila As DataRow In dtHoras.Rows
                    vHoras.Add(fila.Item("cchora"))
                Next
                Dim fecha1 As Date = _dtFechas.Rows(0).Item("ehfec")
                Dim hora1 As String = _dtFechas.Rows(0).Item("ehhor")
                Dim f As Integer = vHoras.IndexOf(hora1)



                _prLimpiar()


                '////////////////////////////////////////
                Dim numiInst2 As String = frmAyuda.filaSelect.Cells("panumi").Value
                Dim dtTbPersonal As DataTable = CType(tbPersona.DataSource, DataTable)
                For i = 0 To dtTbPersonal.Rows.Count - 1
                    If numiInst2 = dtTbPersonal.Rows(i).Item("panumi") Then
                        tbPersona.SelectedIndex = i
                        Exit For
                    End If
                Next
                '////////////////////////////////////////////
                'Dim j As Integer = 0
                'While j < grHorario.SelectedCells.Count
                '    grHorario.SelectedCells(j).Selected = False
                'End While
                'grHorario.Rows(f).Cells(fecha1.Day).Selected = True
                grHorario.CurrentCell = grHorario.Rows(f).Cells(fecha1.Day)


                btnNuevo.Enabled = False
                _prSetHorarioCorrido()
            End If
        Else
            ToastNotification.Show(Me, "no existe ningun instrctor libre en las fechas y horas sleccionadas".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
        End If

    End Sub
#End Region

#Region "Eventos programados manualmente"
    Private Sub tbFechaSelect_ValueChanged(sender As Object, e As EventArgs)
        If tbPersona.SelectedIndex < 0 Then
            Exit Sub
        End If

        If _marcando = False Then
            _prCargarGridAlumnos(tbPersona.Value)
            _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
        End If

    End Sub
#End Region

    Private Sub F0_ClasesPracticas2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub grAlumnos_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grAlumnos.FormattingRow
        'if (e.Row.Cells["ColumnName"].Value == someValue) 
        If e.Row.RowIndex >= 0 Then
            e.Row.Cells("color").FormatStyle = New GridEXFormatStyle
            e.Row.Cells("color").FormatStyle.BackColor = _listColores.Item(e.Row.RowIndex)
        End If

    End Sub

    Private Sub tbPersona_ValueChanged(sender As Object, e As EventArgs) Handles tbPersona.ValueChanged
        If tbPersona.SelectedIndex >= 0 Then
            _prCargarGridAlumnos(tbPersona.Value)
            _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
            btnModificar.Enabled = True
            PanelFechas.Enabled = True
        End If
    End Sub

    Private Sub ADICIONARHORARIOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ADICIONARHORARIOToolStripMenuItem.Click
        'If _AlumSelect < 0 Then
        '    _prSetHorarioCorrido()
        'Else
        '    _prModificarHorarioCorrido()
        'End If

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        If btnEliminar.Visible Then
            _prLimpiar()
        Else
            Close()
        End If

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        _prLimpiar()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        If _AlumSelect < 0 Then
            _prGrabarRegistro()
        Else
            _prModificarRegistro()
        End If
    End Sub

    Private Sub grCabecera_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles grCabecera.ColumnWidthChanged
        grHorario.Columns(e.Column.Index).Width = grCabecera.Columns(e.Column.Index).Width
    End Sub

    Private Sub grHorario_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles grHorario.ColumnWidthChanged
        grCabecera.Columns(e.Column.Index).Width = grHorario.Columns(e.Column.Index).Width
    End Sub

    Private Sub ADICIONARHORARIOMANUALMENTEToolStripMenuItem_Click(sender As Object, e As EventArgs)
        _prSetHorarioManualmente()
    End Sub

    Private Sub grHorario_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grHorario.CellClick


        If _marcarManual = 2 Then
            Dim f As Integer = e.RowIndex
            Dim c As Integer = e.ColumnIndex
            Dim mesSelect As ClsMes = _meses.vmes1

            grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
            grHorario.CurrentRow.Cells(c).Style.ForeColor = Color.Black
            grHorario.CurrentRow.Cells(c).Value = "L"
            _prInsertarTablaLiberarHora(New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, tbPersona.Value)
            _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, _i, 0)
            Exit Sub
        End If


        If _marcarManual = 1 And _i <= _limiteDias Then
            Dim f As Integer = e.RowIndex
            Dim c As Integer = e.ColumnIndex
            Dim mesSelect As ClsMes
            If _mesVisto = 1 Then
                mesSelect = _meses.vmes1
            Else
                mesSelect = _meses.vmes2
            End If

            If IsNothing(mesSelect.vDias(f, c - 1)) Then
                grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
                grHorario.CurrentRow.Cells(c).Value = _i
                _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)

                'cargar dia marcado
                If _mesVisto = 1 Then
                    _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, _i, 0)
                Else
                    _meses._prCargarDiaMes2(f, c - 1, Color.Green, -1, _i, 0)
                End If

                _i = _i + 1
            Else
                If mesSelect.vDias(f, c - 1).estadoCls = conEstClasePermiso Or mesSelect.vDias(f, c - 1).estadoCls = conEstClaseSuspension Or mesSelect.vDias(f, c - 1).estado = conSabado Then
                    grHorario.CurrentRow.Cells(c).Style.BackColor = Color.Green
                    grHorario.CurrentRow.Cells(c).Value = _i
                    _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "1", 0)

                    'cargar dia marcado
                    If _mesVisto = 1 Then
                        _meses._prCargarDiaMes1(f, c - 1, Color.Green, -1, _i, 0)
                    Else
                        _meses._prCargarDiaMes2(f, c - 1, Color.Green, -1, _i, 0)
                    End If

                    _i = _i + 1
                End If
            End If

        End If

    End Sub

    Private Sub btnFAnt_Click(sender As Object, e As EventArgs) Handles btnFAnt.Click
        If tbPersona.SelectedIndex < 0 Then
            tbFechaSelect.Value = DateAdd(DateInterval.Month, -1, tbFechaSelect.Value)
            Exit Sub
        End If

        If _marcando = False Then
            tbFechaSelect.Value = DateAdd(DateInterval.Month, -1, tbFechaSelect.Value)
            ''_prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)'COMENTADO PARA QUITAR TIEMPO DE RESPUESTA
        Else
            If _mesVisto = 2 Then
                tbFechaSelect.Value = DateAdd(DateInterval.Month, -1, tbFechaSelect.Value)
                _prCargarGridHorarioSinReestructurar(1)
                _mesVisto = 1
            End If

        End If
    End Sub


    Private Sub btnFSig_Click(sender As Object, e As EventArgs) Handles btnFSig.Click
        If tbPersona.SelectedIndex < 0 Then
            tbFechaSelect.Value = DateAdd(DateInterval.Month, 1, tbFechaSelect.Value)
            Exit Sub
        End If

        If _marcando = False Then
            tbFechaSelect.Value = DateAdd(DateInterval.Month, 1, tbFechaSelect.Value)
            ''_prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)'COMENTADO PARA QUITAR TIEMPO DE RESPUESTA

        Else
            If _mesVisto = 1 Then
                tbFechaSelect.Value = DateAdd(DateInterval.Month, 1, tbFechaSelect.Value)
                _prCargarGridHorarioSinReestructurar(2)
                _mesVisto = 2
            End If
        End If
    End Sub

    Private Sub grHorario_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles grHorario.CellBeginEdit
        e.Cancel = True
    End Sub


    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        _AlumSelect = grAlumnos.Row
    End Sub



    Private Sub grAlumnos_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grAlumnos.EditingCell
        e.Cancel = True
    End Sub

    Private Sub ASIGNARPERMISOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASIGNARPERMISOToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)

            If IsNothing(hora) = False Then
                If hora.estado = conDiaGrabadoAlumno And (hora.estadoCls = conEstClaseProgramada Or hora.estadoCls = conEstClaseAsistida) Then
                    Dim line As Integer = _meses.vmes1.vDias(f, c - 1).line
                    Dim dtPermisos As DataTable = L_prClasesPracDetFechasGetClasesConPermiso(line)

                    If dtPermisos.Rows.Count > 0 Then
                        Dim diaConflicto As String = "FECHA: " + CType(dtPermisos.Rows(0).Item("ehfec"), Date).ToString("dd/MM/yyyy") + vbCrLf + "HORA: " + dtPermisos.Rows(0).Item("ehhor") + vbCrLf + "OBS: " + dtPermisos.Rows(0).Item("ehobs")
                        Dim info As New TaskDialogInfo("permiso existente".ToUpper, eTaskDialogIcon.Exclamation, "el alumno ya hizo uso de su permiso".ToUpper, "el permiso ya existente es del dia: ".ToUpper + vbCrLf + diaConflicto, eTaskDialogButton.Ok Or eTaskDialogButton.Close, eTaskDialogBackgroundColor.Blue)
                        TaskDialog.Show(info)

                    Else
                        Dim obs As String = InputBox("ingrese alguna observacion".ToUpper, "observacion del permiso".ToUpper, "").ToUpper
                        If obs <> String.Empty Then
                            _dtFechas.Rows.Clear()
                            _dtFechas.Rows.Add(0, 0, line, Nothing, 0, 2, obs, 1)
                            Dim res As Boolean = L_prClasesPracDetalleModificarEstado(_dtFechas)
                            If res Then
                                _dtFechas.Rows.Clear()
                                'hora.ccolor = Color.LightGray
                                'hora.estadoCls = conEstClasePermiso
                                'hora.desc = obs

                                'REENUMERAR
                                'Dim numiAlum As String = hora.numiAlm
                                'Dim dtHorasGrabadas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, numiAlum, tbFechaSelect.Value.ToString("yyyy-MM-dd"))

                                'Dim numero As Integer
                                'Dim dtFechasAnt As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneralContables(tbPersona.Value, numiAlum, DateAdd(DateInterval.Month, -1, tbFechaSelect.Value).ToString("yyyy/MM/dd"))
                                'If dtFechasAnt.Rows.Count = 0 Then
                                '    numero = 1
                                'Else
                                '    numero = dtFechasAnt.Rows.Count + 1
                                'End If
                                'Dim dtFechasAntDeOtroInstructor As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneralContablesMenorAUnaFechaYHoraX(tbPersona.Value, numiAlum, CType(dtHorasGrabadas.Rows(0).Item("ehfec"), Date).ToString("yyyy/MM/dd")) ', dtFechas.Rows(0).Item("ehhor")
                                'numero = numero + dtFechasAntDeOtroInstructor.Rows.Count

                                'For Each filaHora As DataRow In dtHorasGrabadas.Rows
                                '    Dim horaFecha As String = filaHora.Item("ehhor")
                                '    Dim fechaHora As Date = filaHora.Item("ehfec")

                                '    Dim f1 As Integer = _meses.vmes1.vHoras.IndexOf(horaFecha)
                                '    Dim hora1 As ClsHora = _meses.vmes1.vDias(f1, fechaHora.Day - 1)
                                '    If hora1.estadoCls = conEstClaseProgramada Or hora1.estadoCls = conEstClaseFalta Or hora1.estadoCls = conEstClaseAsistida Then
                                '        hora1.numCla = numero
                                '        numero = numero + 1
                                '    End If

                                'Next

                                '_prRepaintGrilla(1)
                                _prCargarGridAlumnos(tbPersona.Value)
                                _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                            End If
                        Else
                            _dtFechas.Rows.Clear()
                            'hora.ccolor = Color.LightGray
                            'hora.estadoCls = conEstClasePermiso

                            ''REENUMERAR
                            'Dim numiAlum As String = hora.numiAlm
                            'Dim dtHorasGrabadas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, numiAlum, tbFechaSelect.Value.ToString("yyyy-MM-dd"))
                            'Dim numero As Integer = 1
                            'For Each filaHora As DataRow In dtHorasGrabadas.Rows
                            '    Dim horaFecha As String = filaHora.Item("ehhor")
                            '    Dim fechaHora As Date = filaHora.Item("ehfec")

                            '    Dim f1 As Integer = _meses.vmes1.vHoras.IndexOf(horaFecha)
                            '    Dim hora1 As ClsHora = _meses.vmes1.vDias(f1, fechaHora.Day - 1)
                            '    If hora1.estadoCls = conEstClaseProgramada Or hora1.estadoCls = conEstClaseFalta Or hora1.estadoCls = conEstClaseAsistida Then
                            '        hora1.numCla = numero
                            '        numero = numero + 1
                            '    End If

                            'Next

                            '_prRepaintGrilla(1)

                            '_prCargarGridAlumnos(tbPersona.Value)
                            '_prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                        End If
                    End If
                End If

            End If

        End If
    End Sub

    Private Sub ASIGNARFALTAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASIGNARFALTAToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)
            Dim line As Integer = _meses.vmes1.vDias(f, c - 1).line

            Dim obs As String = InputBox("ingrese alguna observacion".ToUpper, "observacion de la falta".ToUpper, "").ToUpper
            If obs <> String.Empty Then
                _dtFechas.Rows.Clear()
                _dtFechas.Rows.Add(0, 0, line, Nothing, 0, -1, obs, 1)
                Dim res As Boolean = L_prClasesPracDetalleModificarEstado(_dtFechas)
                If res Then
                    _dtFechas.Rows.Clear()

                    'hora.ccolor = Color.Red
                    'hora.estadoCls = conEstClaseFalta
                    'hora.desc = obs

                    '_prRepaintGrilla(1)
                    _prCargarGridAlumnos(tbPersona.Value)
                    _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                End If
            Else
                _dtFechas.Rows.Clear()

                'hora.ccolor = Color.Red
                'hora.estadoCls = conEstClaseFalta

                '_prRepaintGrilla(1)

                '_prCargarGridAlumnos(tbPersona.Value)
                '_prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
            End If

        End If
    End Sub

    Private Sub ASIGARCLASEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASIGARCLASEToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)
            If IsNothing(hora) = False Then
                If hora.estado = conCumple Or hora.estado = conFeriado Or hora.estado = conDiaMarcado Or (hora.estado = conDiaGrabadoAlumno And hora.estadoCls <> conEstClasePermiso And hora.estadoCls <> conEstClaseSuspension) Then
                    Exit Sub
                End If
            End If

            _dtFechas.Rows.Clear()
            _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)

            'grabar horario
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable = L_prAlumnoLibresInstructorFaltanClasesAyuda(tbSuc.Value, tbPersona.Value) 'gi_userSuc
            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("cbnumi", True, "Codigo".ToUpper, 70))
            listEstCeldas.Add(New Modelos.Celda("cbci", True, "CI".ToUpper, 70))
            listEstCeldas.Add(New Modelos.Celda("cbnom2", True, "Nombre completo".ToUpper, 300))

            frmAyuda = New Modelos.ModeloAyuda(600, 300, dt, "Seleccione el estudiante a quien se programara el horario".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()
            Dim estadoTipoClase As Integer = 1 'grAlumnos.GetValue("egest")
            If frmAyuda.seleccionado = True Then
                Dim numiAalumno As String = frmAyuda.filaSelect.Cells("cbnumi").Value
                Dim numiReg As String = ""
                Dim respuesta As Boolean = L_prClasesPracDetalleGrabar2(numiReg, tbPersona.Value, numiAalumno, estadoTipoClase, _cantClasesPracticas, _cantClasesReforzamiento, _dtFechas)
                If respuesta = True Then
                    _dtFechas.Rows.Clear()
                    _prCargarGridAlumnos(tbPersona.Value)
                    _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)

                    btnNuevo.Enabled = False
                    tbFechaSelect.Enabled = True
                    '_limiteDias = 15
                    _limiteDias = _cantClasesPracticas
                End If
            End If
        End If



    End Sub

    Private Sub AGREGAROBSERVACIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AGREGAROBSERVACIONToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)

            If IsNothing(hora) = False Then
                If hora.estado = 1 Then 'pregunto si es un dia ya grabado del alumno
                    Dim line As Integer = hora.line
                    Dim estadoCls As Integer = hora.estadoCls

                    Dim obs As String = InputBox("ingrese alguna observacion".ToUpper, "observacion del permiso".ToUpper, "").ToUpper
                    If obs <> String.Empty Then
                        _dtFechas.Rows.Clear()
                        _dtFechas.Rows.Add(0, 0, line, Nothing, 0, estadoCls, obs, 1)
                        Dim res As Boolean = L_prClasesPracDetalleModificarEstado(_dtFechas)
                        If res Then
                            _dtFechas.Rows.Clear()

                            'hora.desc = obs

                            '_prRepaintGrilla(1)
                            _prCargarGridAlumnos(tbPersona.Value)
                            _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                        End If
                    Else
                        _dtFechas.Rows.Clear()
                        'hora.desc = obs

                        '_prRepaintGrilla(1)

                        '_prCargarGridAlumnos(tbPersona.Value)
                        '_prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                    End If
                Else
                    ToastNotification.Show(Me, "seleccione una casilla que no sea un Día o fecha inhábil".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                End If
            Else
                ToastNotification.Show(Me, "seleccione una casila que ya tenga una asignacion".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            End If


        End If
    End Sub

    Private Sub grHorario_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grHorario.CellDoubleClick
        If _marcando = False Then
            Dim f, c As Integer
            f = grHorario.CurrentCell.RowIndex
            c = grHorario.CurrentCell.ColumnIndex

            If f >= 0 And c >= 1 Then
                Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)
                If IsNothing(hora) = False Then
                    If hora.claseSobrepuesta = True Then

                        Dim horaClase As String = grHorario.Rows(f).Cells(0).Value

                        Dim frmAyuda As Modelos.ModeloAyuda
                        Dim dtHora As DataTable = L_prClasesPracDetFechasGetPorFechaHoraCompleto(New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), horaClase, tbPersona.Value)
                        Dim listEstCeldas As New List(Of Modelos.Celda)
                        listEstCeldas.Add(New Modelos.Celda("nomAlum", True, "Alumno".ToUpper, 200))
                        listEstCeldas.Add(New Modelos.Celda("nomInst", False, "Instructor".ToUpper, 300))
                        listEstCeldas.Add(New Modelos.Celda("ehnumi", False))
                        listEstCeldas.Add(New Modelos.Celda("ehtser", False))
                        listEstCeldas.Add(New Modelos.Celda("ehlin", False))
                        listEstCeldas.Add(New Modelos.Celda("ehfec", True, "fecha".ToUpper, 100))
                        listEstCeldas.Add(New Modelos.Celda("ehhor", True, "hora".ToUpper, 80))
                        listEstCeldas.Add(New Modelos.Celda("ehest", False))
                        listEstCeldas.Add(New Modelos.Celda("ehest2", True, "estado".ToUpper, 100))
                        listEstCeldas.Add(New Modelos.Celda("ehobs", True, "observacion".ToUpper, 200))


                        frmAyuda = New Modelos.ModeloAyuda(350, 0, dtHora, "Clases sobrepuestas".ToUpper, listEstCeldas, 200)
                        frmAyuda.ShowDialog()
                        Exit Sub
                    End If


                    If hora.estado = 1 Or hora.estado = -3 Or hora.estado = conHoraLiberada Or hora.estado = conHoraDiligencia Then
                        ToastNotification.Show(Me, hora.desc.ToUpper, My.Resources.INFORMATION, 5000, eToastGlowColor.Blue, eToastPosition.TopCenter)
                    End If

                End If

            End If


        End If

    End Sub

    Private Sub VEROBSERVACIONESDECLASESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VEROBSERVACIONESDECLASESToolStripMenuItem.Click
        Dim f As Integer
        f = grAlumnos.Row

        If f >= 0 Then

            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dtFechas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, grAlumnos.GetValue("cbnumi"), tbFechaSelect.Value.ToString("yyyy-MM-dd"))
            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("ehnumi", False))
            listEstCeldas.Add(New Modelos.Celda("ehtser", False))
            listEstCeldas.Add(New Modelos.Celda("ehlin", False))
            listEstCeldas.Add(New Modelos.Celda("ehfec", True, "fecha".ToUpper, 100))
            listEstCeldas.Add(New Modelos.Celda("ehhor", True, "hora".ToUpper, 80))
            listEstCeldas.Add(New Modelos.Celda("ehest", False))
            listEstCeldas.Add(New Modelos.Celda("ehest2", True, "estado".ToUpper, 100))
            listEstCeldas.Add(New Modelos.Celda("ehobs", True, "observacion".ToUpper, 300))

            frmAyuda = New Modelos.ModeloAyuda(600, 300, dtFechas, "detalle de observaciones de los horarios".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()
        End If



    End Sub

    Private Sub tbSuc_ValueChanged(sender As Object, e As EventArgs) Handles tbSuc.ValueChanged
        If tbSuc.SelectedIndex >= 0 Then
            'codigo para hacer el numero de clases por  sucursal--------------------------------
            Dim dtSuc As DataTable = L_prSucursalAyudaPorNumi(tbSuc.Value)
            _cantClasesPracticas = dtSuc.Rows(0).Item("canprac")
            _cantClasesReforzamiento = dtSuc.Rows(0).Item("canrefor")
            '-----------------------------------------------------------------------------------

            _prCargarComboInstructores()
            tbPersona.SelectedIndex = -1
            grAlumnos.DataSource = Nothing
            grHorario.DataSource = Nothing
        End If
    End Sub


    Private Sub ASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASToolStripMenuItem.Click
        _prAsignarAsistencia()
    End Sub

    Private Sub ASIGNARTODASLASASISTENCIASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASIGNARTODASLASASISTENCIASToolStripMenuItem.Click
        _prAsignarTodasLasAsistencias()
    End Sub

    Private Sub grAlumnos_SelectionChanged(sender As Object, e As EventArgs) Handles grAlumnos.SelectionChanged
        If Not IsNothing(grHorario.DataSource) Then
            _prSeleccionarHoras()
        End If

    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If tbSuc.SelectedIndex < 0 Or tbPersona.SelectedIndex < 0 Then
            ToastNotification.Show(Me, "tiene que estar seleccionado el instructor y la sucursal".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Red, eToastPosition.TopCenter)
            Exit Sub
        End If

        If PanelToolBar1.Visible = False Then
            _prLiberarHoraGrabar()
        Else
            _prLiberarHoraEmpezar()
        End If

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        _prVerDisponibilidad()
    End Sub

    Private Sub ADICIONARHORARIOToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ADICIONARHORARIOToolStripMenuItem1.Click
        _prSetHorarioCorrido()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        _prMostrarChoferesDisponibles()
    End Sub

    Private Sub tbFechaSelect_ValueChanged_1(sender As Object, e As EventArgs) Handles tbFechaSelect.ValueChanged

    End Sub

    Private Sub ADICIONARHORARIOToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ADICIONARHORARIOToolStripMenuItem2.Click
        If _AlumSelect < 0 Then
            _prSetHorarioCorrido()
        Else
            _prModificarHorarioCorrido()
        End If
    End Sub

    Private Sub ADICIONARMANUALMENTEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ADICIONARMANUALMENTEToolStripMenuItem.Click
        _limiteDias = _cantClasesPracticas
        _isClasePractica = True
        _prSetHorarioManualmente()
    End Sub

    Private Sub ADICIONARHORARIOToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ADICIONARHORARIOToolStripMenuItem3.Click

        _prSetHorarioCorridoReforzamiento()
    End Sub

    Private Sub ADICIONARMANUALMENTEToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ADICIONARMANUALMENTEToolStripMenuItem1.Click
        _limiteDias = _cantClasesReforzamiento
        _isClasePractica = False
        _prSetHorarioManualmente()
    End Sub

    Private Sub ASIGNARSUSPENCIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASIGNARSUSPENCIONToolStripMenuItem.Click


    End Sub

    Private Sub FINALIZARCURSOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FINALIZARCURSOToolStripMenuItem.Click
        Dim f As Integer
        f = grAlumnos.Row

        If f >= 0 Then
            Dim estado As Integer = grAlumnos.GetValue("egest")
            Dim numiCab As String = grAlumnos.GetValue("egnumi")

            Dim res As Boolean = L_prClasesPracModificarEstadoCabecera(numiCab, (estado * -1).ToString.Trim)
            If res Then
                ToastNotification.Show(Me, "se finalizo la clase seleccionado".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _dtFechas.Rows.Clear()
                _prCargarGridAlumnos(tbPersona.Value)
                _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
            Else
                ToastNotification.Show(Me, "no se pudo realizar la operacion".ToUpper, My.Resources.WARNING, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            End If

        End If
    End Sub

    Private Sub VOLVERAPROGRAMARCLASEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VOLVERAPROGRAMARCLASEToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)

            If IsNothing(hora) = False Then
                If hora.estado = conDiaGrabadoAlumno And (hora.estadoCls = conEstClaseAsistida Or hora.estadoCls = conEstClaseFalta Or hora.estadoCls = conEstClasePermiso Or hora.estadoCls = conEstClaseSuspension) Then
                    Dim line As Integer = _meses.vmes1.vDias(f, c - 1).line

                    Dim obs As String = InputBox("ingrese alguna observacion".ToUpper, "observacion de la reprogramacion".ToUpper, "").ToUpper
                    If obs <> String.Empty Then
                        _dtFechas.Rows.Clear()
                        _dtFechas.Rows.Add(0, 0, line, Nothing, 0, conEstClaseProgramada, obs, 1)
                        Dim res As Boolean = L_prClasesPracDetalleModificarEstado(_dtFechas)
                        If res Then
                            _dtFechas.Rows.Clear()
                            'buscar color
                            'Dim dtAlumnosGrilla As DataTable = CType(grAlumnos.DataSource, DataTable)
                            'Dim colorBuscado As String = ""
                            'For Each filaAlumno As DataRow In dtAlumnosGrilla.Rows
                            '    If filaAlumno.Item("cbnumi") = hora.numiAlm Then
                            '        colorBuscado = filaAlumno.Item("color")
                            '        Exit For
                            '    End If
                            'Next
                            'hora.ccolor = Color.FromArgb(colorBuscado)
                            'hora.estadoCls = conEstClaseProgramada
                            'hora.desc = obs

                            ''REENUMERAR
                            'Dim numiAlum As String = hora.numiAlm
                            'Dim dtHorasGrabadas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, numiAlum, tbFechaSelect.Value.ToString("yyyy-MM-dd"))

                            'Dim numero As Integer
                            'Dim dtFechasAnt As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneralContables(tbPersona.Value, numiAlum, DateAdd(DateInterval.Month, -1, tbFechaSelect.Value).ToString("yyyy/MM/dd"))
                            'If dtFechasAnt.Rows.Count = 0 Then
                            '    numero = 1
                            'Else
                            '    numero = dtFechasAnt.Rows.Count + 1
                            'End If
                            'Dim dtFechasAntDeOtroInstructor As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneralContablesMenorAUnaFechaYHoraX(tbPersona.Value, numiAlum, CType(dtHorasGrabadas.Rows(0).Item("ehfec"), Date).ToString("yyyy/MM/dd")) ', dtFechas.Rows(0).Item("ehhor")
                            'numero = numero + dtFechasAntDeOtroInstructor.Rows.Count

                            'For Each filaHora As DataRow In dtHorasGrabadas.Rows
                            '    Dim horaFecha As String = filaHora.Item("ehhor")
                            '    Dim fechaHora As Date = filaHora.Item("ehfec")

                            '    Dim f1 As Integer = _meses.vmes1.vHoras.IndexOf(horaFecha)
                            '    Dim hora1 As ClsHora = _meses.vmes1.vDias(f1, fechaHora.Day - 1)
                            '    If hora1.estadoCls = conEstClaseProgramada Or hora1.estadoCls = conEstClaseFalta Or hora1.estadoCls = conEstClaseAsistida Then
                            '        hora1.numCla = numero
                            '        numero = numero + 1
                            '    End If

                            'Next

                            '_prRepaintGrilla(1)
                            _prCargarGridAlumnos(tbPersona.Value)
                            _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                        End If
                    Else
                        _dtFechas.Rows.Clear()
                        '_prCargarGridAlumnos(tbPersona.Value)
                        '_prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub ASGINARSUSPENCIONASOLOUNACLASEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASGINARSUSPENCIONASOLOUNACLASEToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)

            If IsNothing(hora) = False Then
                If hora.estado = conDiaGrabadoAlumno And (hora.estadoCls = conEstClaseProgramada Or hora.estadoCls = conEstClaseAsistida) Then
                    Dim line As Integer = _meses.vmes1.vDias(f, c - 1).line

                    Dim obs As String = InputBox("ingrese alguna observacion".ToUpper, "observacion del permiso".ToUpper, "").ToUpper
                    If obs <> String.Empty Then
                        _dtFechas.Rows.Clear()
                        _dtFechas.Rows.Add(0, 0, line, Nothing, 0, conEstClaseSuspension, obs, 1)
                        Dim res As Boolean = L_prClasesPracDetalleModificarEstado(_dtFechas)
                        If res Then
                            _dtFechas.Rows.Clear()
                            'hora.ccolor = Color.LightGray
                            'hora.estadoCls = conEstClaseSuspension
                            'hora.desc = obs

                            ''REENUMERAR
                            'Dim numiAlum As String = hora.numiAlm
                            'Dim dtHorasGrabadas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, numiAlum, tbFechaSelect.Value.ToString("yyyy-MM-dd"))

                            'Dim numero As Integer
                            'Dim dtFechasAnt As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneralContables(tbPersona.Value, numiAlum, DateAdd(DateInterval.Month, -1, tbFechaSelect.Value).ToString("yyyy/MM/dd"))
                            'If dtFechasAnt.Rows.Count = 0 Then
                            '    numero = 1
                            'Else
                            '    numero = dtFechasAnt.Rows.Count + 1
                            'End If
                            'Dim dtFechasAntDeOtroInstructor As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneralContablesMenorAUnaFechaYHoraX(tbPersona.Value, numiAlum, CType(dtHorasGrabadas.Rows(0).Item("ehfec"), Date).ToString("yyyy/MM/dd")) ', dtFechas.Rows(0).Item("ehhor")
                            'numero = numero + dtFechasAntDeOtroInstructor.Rows.Count

                            'For Each filaHora As DataRow In dtHorasGrabadas.Rows
                            '    Dim horaFecha As String = filaHora.Item("ehhor")
                            '    Dim fechaHora As Date = filaHora.Item("ehfec")

                            '    Dim f1 As Integer = _meses.vmes1.vHoras.IndexOf(horaFecha)
                            '    Dim hora1 As ClsHora = _meses.vmes1.vDias(f1, fechaHora.Day - 1)
                            '    If hora1.estadoCls = conEstClaseProgramada Or hora1.estadoCls = conEstClaseFalta Or hora1.estadoCls = conEstClaseAsistida Then
                            '        hora1.numCla = numero
                            '        numero = numero + 1
                            '    End If

                            'Next

                            '_prRepaintGrilla(1)
                            _prCargarGridAlumnos(tbPersona.Value)
                            _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                        End If
                    Else
                        _dtFechas.Rows.Clear()

                        'hora.ccolor = Color.LightGray
                        'hora.estadoCls = conEstClaseSuspension

                        ''REENUMERAR
                        'Dim numiAlum As String = hora.numiAlm
                        'Dim dtHorasGrabadas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, numiAlum, tbFechaSelect.Value.ToString("yyyy-MM-dd"))
                        'Dim numero As Integer = 1
                        'For Each filaHora As DataRow In dtHorasGrabadas.Rows
                        '    Dim horaFecha As String = filaHora.Item("ehhor")
                        '    Dim fechaHora As Date = filaHora.Item("ehfec")

                        '    Dim f1 As Integer = _meses.vmes1.vHoras.IndexOf(horaFecha)
                        '    Dim hora1 As ClsHora = _meses.vmes1.vDias(f1, fechaHora.Day - 1)
                        '    If hora1.estadoCls = conEstClaseProgramada Or hora1.estadoCls = conEstClaseFalta Or hora1.estadoCls = conEstClaseAsistida Then
                        '        hora1.numCla = numero
                        '        numero = numero + 1
                        '    End If

                        'Next

                        '_prRepaintGrilla(1)

                        '_prCargarGridAlumnos(tbPersona.Value)
                        '_prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub ASGINARSUSPENCIONATODASLASCLASESENADELANTEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASGINARSUSPENCIONATODASLASCLASESENADELANTEToolStripMenuItem.Click

        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)

            If IsNothing(hora) = False Then
                If hora.estado = conDiaGrabadoAlumno And (hora.estadoCls = conEstClaseProgramada Or hora.estadoCls = conEstClaseAsistida) Then
                    Dim horaSelect As ClsHora = _meses.vmes1.vDias(f, c - 1)
                    Dim numiAlum As String = horaSelect.numiAlm

                    'Dim line As Integer = _meses.vmes1.vDias(f, c - 1).line

                    Dim obs As String = InputBox("ingrese alguna observacion".ToUpper, "observacion del permiso".ToUpper, "").ToUpper
                    If obs <> String.Empty Then
                        'consulto todas las clases que tiene ese alumno
                        Dim dtHorasGrabadas As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, numiAlum, tbFechaSelect.Value.ToString("yyyy-MM-dd"))
                        Dim i As Integer = 0
                        Dim horaEnFila As String = grHorario.Rows(f).Cells(0).Value
                        _dtFechas.Rows.Clear()
                        While i <= dtHorasGrabadas.Rows.Count - 1
                            Dim fechaGrabada As String = CType(dtHorasGrabadas.Rows(i).Item("ehfec"), Date).ToString("yyyy/MM/dd")
                            Dim horaGrabada As String = dtHorasGrabadas.Rows(i).Item("ehhor")

                            If New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd") = fechaGrabada And horaGrabada = horaEnFila Then
                                Exit While
                            End If
                            i = i + 1

                        End While

                        While i <= dtHorasGrabadas.Rows.Count - 1
                            Dim line As String = dtHorasGrabadas.Rows(i).Item("ehlin")
                            _dtFechas.Rows.Add(0, 0, line, Nothing, 0, conEstClaseSuspension, obs, 1)
                            i = i + 1
                        End While

                        'consulto las fechas del proximo mes
                        Dim dtHorasGrabadasDelProximoMes As DataTable = L_prClasesPracDetFechasPorAlumnoYFechaGeneral(tbPersona.Value, numiAlum, DateAdd(DateInterval.Month, 1, tbFechaSelect.Value).ToString("yyyy-MM-dd"))
                        For Each filaProxMes As DataRow In dtHorasGrabadasDelProximoMes.Rows
                            Dim line As String = filaProxMes.Item("ehlin")
                            _dtFechas.Rows.Add(0, 0, line, Nothing, 0, conEstClaseSuspension, obs, 1)
                        Next

                        Dim res As Boolean = L_prClasesPracDetalleModificarEstado(_dtFechas)
                        If res Then
                            _dtFechas.Rows.Clear()
                            _prCargarGridAlumnos(tbPersona.Value)
                            _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                        End If
                    Else
                        _dtFechas.Rows.Clear()
                        _prCargarGridAlumnos(tbPersona.Value)
                        _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                    End If

                End If
            End If
        End If



    End Sub

    Private Sub ASIGNARCLASEREFORZAMIENTOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ASIGNARCLASEREFORZAMIENTOToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)
            If IsNothing(hora) = False Then
                If hora.estado = conCumple Or hora.estado = conFeriado Or hora.estado = conDiaMarcado Or (hora.estado = conDiaGrabadoAlumno And hora.estadoCls <> conEstClasePermiso And hora.estadoCls <> conEstClaseSuspension) Then
                    Exit Sub
                End If
            End If

            _dtFechas.Rows.Clear()
            _dtFechas.Rows.Add(1, 1, 1, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, 0, "", 0)

            'grabar horario
            Dim frmAyuda As Modelos.ModeloAyuda
            Dim dt As DataTable = L_prAlumnoLibresInstructorFaltanClasesAyuda(tbSuc.Value, tbPersona.Value) 'gi_userSuc
            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("cbnumi", True, "Codigo".ToUpper, 70))
            listEstCeldas.Add(New Modelos.Celda("cbci", True, "CI".ToUpper, 70))
            listEstCeldas.Add(New Modelos.Celda("cbnom2", True, "Nombre completo".ToUpper, 300))

            frmAyuda = New Modelos.ModeloAyuda(600, 300, dt, "Seleccione el estudiante a quien se programara el horario".ToUpper, listEstCeldas)
            frmAyuda.ShowDialog()
            Dim estadoTipoClase As Integer = 2 'grAlumnos.GetValue("egest")
            If frmAyuda.seleccionado = True Then
                Dim numiAalumno As String = frmAyuda.filaSelect.Cells("cbnumi").Value
                Dim numiReg As String = ""
                Dim respuesta As Boolean = L_prClasesPracDetalleGrabar2(numiReg, tbPersona.Value, numiAalumno, estadoTipoClase, _cantClasesPracticas, _cantClasesReforzamiento, _dtFechas)
                If respuesta = True Then
                    _dtFechas.Rows.Clear()
                    _prCargarGridAlumnos(tbPersona.Value)
                    _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)

                    btnNuevo.Enabled = False
                    tbFechaSelect.Enabled = True
                    '_limiteDias = 15
                    _limiteDias = _cantClasesPracticas
                End If
            End If
        End If


    End Sub

    Private Sub ELIMINARLIBERACIONDEHORAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARLIBERACIONDEHORAToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)
            If IsNothing(hora) = False Then
                If hora.estado = conHoraLiberada Then
                    Dim numi As String = hora.line
                    Dim resp As Boolean
                    resp = L_prHoraLibreTCE0062Eliminar(numi)
                    If resp = True Then
                        _prCargarGridAlumnos(tbPersona.Value)
                        _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                    End If
                End If
            End If


        End If
    End Sub

    Private Sub AGREGARHORADILIGENCIAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AGREGARHORADILIGENCIAToolStripMenuItem.Click


    End Sub

    Private Sub AGREGARHORADILIGENCIAToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AGREGARHORADILIGENCIAToolStripMenuItem1.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)
            If IsNothing(hora) = False Then
                If hora.estado = conCumple Or hora.estado = conFeriado Or hora.estado = conDiaMarcado Or hora.estado = conHoraLiberada Or hora.estado = conHoraDiligencia Or (hora.estado = conDiaGrabadoAlumno And hora.estadoCls <> conEstClasePermiso And hora.estadoCls <> conEstClaseSuspension) Then
                    Exit Sub
                End If
            End If

            Dim dtHorasDiligencia As DataTable
            dtHorasDiligencia = New DataTable
            dtHorasDiligencia.Columns.Add("ehilin", GetType(Integer))
            dtHorasDiligencia.Columns.Add("ehichof", GetType(Integer))
            dtHorasDiligencia.Columns.Add("ehifec", GetType(Date))
            dtHorasDiligencia.Columns.Add("ehihor", GetType(String))
            dtHorasDiligencia.Columns.Add("ehiobs", GetType(String))
            dtHorasDiligencia.Columns.Add("estado", GetType(Integer))

            Dim obs As String = InputBox("ingrese alguna observacion".ToUpper, "observacion de diligencia".ToUpper, "").ToUpper
            If obs <> String.Empty Then
                _prInsertarTablaHoraDiligencia(dtHorasDiligencia, New Date(tbFechaSelect.Value.Year, tbFechaSelect.Value.Month, c).ToString("yyyy/MM/dd"), grHorario.CurrentRow.Cells("hora").Value, tbPersona.Value)
                Dim respuesta As Boolean = L_prHoraDiligenciaTCE0063GrabarPorInstructor(dtHorasDiligencia, obs)
                If respuesta = True Then
                    _dtFechas.Rows.Clear()
                    _prCargarGridAlumnos(tbPersona.Value)
                    _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)

                    btnNuevo.Enabled = False
                    tbFechaSelect.Enabled = True

                End If

            End If

        End If
    End Sub

    Private Sub grHorario_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grHorario.CellContentClick

    End Sub

    Private Sub ELIMINARHORADILIGENCIAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ELIMINARHORADILIGENCIAToolStripMenuItem.Click
        Dim f, c As Integer
        f = grHorario.CurrentCell.RowIndex
        c = grHorario.CurrentCell.ColumnIndex

        If f >= 0 And c >= 1 Then
            Dim hora As ClsHora = _meses.vmes1.vDias(f, c - 1)
            If IsNothing(hora) = False Then
                If hora.estado = conHoraDiligencia Then
                    Dim numi As String = hora.line
                    Dim resp As Boolean
                    resp = L_prHoraDiligenciaTCE0063Eliminar(numi)
                    If resp = True Then
                        _prCargarGridAlumnos(tbPersona.Value)
                        _prCargarGridHorario(tbFechaSelect.Value, tbPersona.Value)
                    End If
                End If
            End If


        End If
    End Sub
End Class