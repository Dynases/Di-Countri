Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Public Class F1_Bancos

#Region "Variable Globales"

    Dim Modificado As Boolean = False
    Dim Socio As Boolean = False
    Public _nameButton As String
#End Region

#Region "METODOS PRIVADOS"

    Private Sub _prIniciarTodo()

        Me.Text = "B A N C O S"
        _PMIniciarTodo()
        _prAsignarPermisos()
        _prCargarLengthTextBox()
        GroupPanelBuscador.Style.BackColor = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GroupPanelBuscador.Style.TextColor = Color.White
        Dim blah As Bitmap = My.Resources.cliente
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        JGrM_Buscador.RootTable.HeaderFormatStyle.FontBold = TriState.True
        JGrM_Buscador.AlternatingColors = True
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
    End Sub

    Public Sub _prCargarLengthTextBox()
        tbnombre.MaxLength = 200
        tbcuenta.MaxLength = 200
        tbobservacion.MaxLength = 200
    End Sub



    Private Function _fnActionNuevo() As Boolean
        'Funcion que me devuelve True si esta en la actividad crear nuevo Tipo de Equipo
        Return tbcodigo.Text.ToString.Equals("") And tbnombre.ReadOnly = False
    End Function

    Private Sub _prAsignarPermisos()

        'Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        'Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        'Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        'Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        'Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        'If add = False Then
        '    btnNuevo.Visible = False
        'End If
        'If modif = False Then
        '    btnModificar.Visible = False
        'End If
        'If del = False Then
        '    btnEliminar.Visible = False
        'End If

    End Sub



#End Region

#Region "METODOS SOBREESCRITOS"



    Public Overrides Sub _PMOHabilitar()
        tbnombre.ReadOnly = False
        tbcuenta.ReadOnly = False
        tbobservacion.ReadOnly = False

    End Sub
    Public Overrides Sub _PMOInhabilitar()
        tbnombre.ReadOnly = True
        tbcuenta.ReadOnly = True
        tbobservacion.ReadOnly = True
        tbcodigo.ReadOnly = True
    End Sub
    Public Overrides Sub _PMOHabilitarFocus()
        With MHighlighterFocus
            .SetHighlightOnFocus(tbnombre, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbcuenta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbobservacion, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)
            .SetHighlightOnFocus(tbcodigo, DevComponents.DotNetBar.Validator.eHighlightColor.Blue)

        End With
    End Sub
    Public Overrides Sub _PMOLimpiar()


        tbcodigo.Text = ""
        tbnombre.Text = ""
        tbcuenta.Text = ""
        tbobservacion.Text = ""

        tbnombre.Focus()

    End Sub
    Public Overrides Sub _PMOLimpiarErrores()
        MEP.Clear()
        tbcodigo.BackColor = Color.White
        tbnombre.BackColor = Color.White
        tbcuenta.BackColor = Color.White
        tbobservacion.BackColor = Color.White
    End Sub
    Public Overrides Function _PMOValidarCampos() As Boolean
        Dim _ok As Boolean = True
        MEP.Clear()

        'If tbCi.Text = String.Empty Then
        '    tbCi.BackColor = Color.Red
        '    MEP.SetError(tbCi, "ingrese Cedula de Identidad!".ToUpper)
        '    _ok = False
        'Else
        '    tbCi.BackColor = Color.White
        '    MEP.SetError(tbCi, "")
        'End If

        'If tbFnac.Text = String.Empty Then
        '    tbFnac.BackColor = Color.Red
        '    MEP.SetError(tbFnac, "Seleccione su fecha de nacimiento!".ToUpper)
        '    _ok = False
        'Else
        '    tbFnac.BackColor = Color.White
        '    MEP.SetError(tbFnac, "")
        'End If

        If tbnombre.Text = String.Empty Then
            tbnombre.BackColor = Color.Red
            MEP.SetError(tbnombre, "ingrese Dato en el campo Nombre !".ToUpper)
            _ok = False
        Else
            tbnombre.BackColor = Color.White
            MEP.SetError(tbnombre, "")
        End If

        'If tbDir.Text = String.Empty Then
        '    tbDir.BackColor = Color.Red
        '    MEP.SetError(tbDir, "ingrese su Direccion de domicilio o lugar donde vive!".ToUpper)
        '    _ok = False
        'Else
        '    tbDir.BackColor = Color.White
        '    MEP.SetError(tbDir, "")
        'End If
        'If tbEmail.Text = String.Empty Then
        '    tbEmail.BackColor = Color.Red
        '    MEP.SetError(tbEmail, "ingrese su direccion de correo electronico!".ToUpper)
        '    _ok = False
        'Else
        '    tbEmail.BackColor = Color.White
        '    MEP.SetError(tbEmail, "")
        'End If
        'If tbTel1.Text = String.Empty Then
        '    tbTel1.BackColor = Color.Red
        '    MEP.SetError(tbTel1, "ingrese su numero de Telefono o Celular!".ToUpper)
        '    _ok = False
        'Else
        '    tbTel1.BackColor = Color.White
        '    MEP.SetError(tbTel1, "")
        'End If


        MHighlighterFocus.UpdateHighlights()
        Return _ok
    End Function
    Public Overrides Function _PMOGetListEstructuraBuscador() As List(Of Modelos.Celda)
        Dim listEstCeldas As New List(Of Modelos.Celda)

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        listEstCeldas.Add(New Modelos.Celda("canumi", True, "CODIGO", 150))
        listEstCeldas.Add(New Modelos.Celda("canombre", True, "NOMBRE", 280))
        listEstCeldas.Add(New Modelos.Celda("cacuenta", True, "CUENTA", 220))
        listEstCeldas.Add(New Modelos.Celda("caobs", True, "OBSERVACION", 350))
        listEstCeldas.Add(New Modelos.Celda("cafact", False))
        listEstCeldas.Add(New Modelos.Celda("cahact", False))
        listEstCeldas.Add(New Modelos.Celda("cauact", False))

        Return listEstCeldas


    End Function
    Public Overrides Function _PMOGetTablaBuscador() As DataTable
        Dim dtBuscador As DataTable = L_prBancoGeneral()
        Return dtBuscador
    End Function



    Public Overrides Sub _PMOMostrarRegistro(_N As Integer)
        JGrM_Buscador.Row = _MPos

        't.canumi , t.canombre, t.cacuenta, t.caobs, t.cafact, t.cahact, t.cauact 
        With JGrM_Buscador
            tbcodigo.Text = .GetValue("canumi").ToString
            tbnombre.Text = .GetValue("canombre").ToString
            tbcuenta.Text = .GetValue("cacuenta").ToString
            tbobservacion.Text = .GetValue("caobs").ToString
            lbFecha.Text = CType(.GetValue("cafact"), Date).ToString("dd/MM/yyyy")
            lbHora.Text = .GetValue("cahact").ToString
            lbUsuario.Text = .GetValue("cauact").ToString

        End With

        LblPaginacion.Text = Str(_MPos + 1) + "/" + JGrM_Buscador.RowCount.ToString




    End Sub
    Public Overrides Function _PMOGrabarRegistro() As Boolean



        Dim res As Boolean = L_prBancoGrabar(tbcodigo.Text, tbnombre.Text, tbcuenta.Text, tbobservacion.Text)
        If res Then
            Modificado = False


            ToastNotification.Show(Me, "Codigo de BANCO ".ToUpper + tbcodigo.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res

    End Function
    Public Overrides Function _PMOModificarRegistro() As Boolean
        Dim tipo As Integer = 1
        Dim nsoc As Integer = 1
        Dim res As Boolean



        res = L_prBancoModificar(tbcodigo.Text, tbnombre.Text, tbcuenta.Text, tbobservacion.Text)


        If res Then


            If (Modificado = True) Then

                Modificado = False
            End If

            ToastNotification.Show(Me, "Codigo de BANCO ".ToUpper + tbcodigo.Text + " modificado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
        End If
        Return res
    End Function

    Public Overrides Sub _PMOEliminarRegistro()
        Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prBancoBorrar(tbcodigo.Text, mensajeError)
            If res Then


                ToastNotification.Show(Me, "Codigo de Banco ".ToUpper + tbcodigo.Text + " eliminado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
                _PMFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If
    End Sub

    Private Sub F1_Bancos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        tbnombre.Focus()

    End Sub
#End Region




End Class