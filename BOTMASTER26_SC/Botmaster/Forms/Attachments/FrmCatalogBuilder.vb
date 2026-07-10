Public Class FrmCatalogBuilder
    Private Catalog As WACatalogDetails
    Public CatalogFileName As String = ""
    Private Sub FrmCatalogBuilder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try : ThemeManager.ApplyTheme(Me) : Catch : End Try
        ReturnedCatalogFileName = ""
        If CatalogFileName <> "" Then
            Try
                Catalog = Newtonsoft.Json.JsonConvert.DeserializeObject(Of WACatalogDetails)(IO.File.ReadAllText(CatalogFileName))
                TextBox1.Text = Catalog.title
                TextBox2.Text = Catalog.description
                TextBox3.Text = Catalog.footer
                TextBox4.Text = Catalog.buttonText
                For Each cat In Catalog.sections
                    Dim Category As New CatalogCategory
                    Category.CategoryName = cat.title
                    Category.LoadRow(cat.rows)
                    FlowLayoutPanel1.Controls.Add(Category)
                Next

            Catch ex As Exception
                MsgBox(ex.Message, vbCritical, Application.ProductName)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim catalog As New WACatalogDetails
        catalog.title = TextBox1.Text
        catalog.description = TextBox2.Text
        catalog.footer = TextBox3.Text
        catalog.buttonText = TextBox4.Text
        Dim category As WAcatalog
        Dim Result As New List(Of WAcatalog)
        For Each cat As CatalogCategory In FlowLayoutPanel1.Controls
            category = New WAcatalog
            category.title = cat.CategoryName
            category.rows = cat.GetItems
            Result.Add(category)
        Next
        catalog.sections = Result

        If CatalogFileName <> "" Then
            IO.File.WriteAllText(CatalogFileName, Newtonsoft.Json.JsonConvert.SerializeObject(catalog))
        Else
            Dim JsonCatalog As String = Newtonsoft.Json.JsonConvert.SerializeObject(catalog)
            Dim CatalogName As String = $"Catalog_{Now.Ticks}.catalog"
            ReturnedCatalogFileName = ClsSpecialDirectories.GetCatalog() & CatalogName
            IO.File.WriteAllText(ReturnedCatalogFileName, JsonCatalog)
        End If




        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim catalog As New WACatalogDetails
        catalog.title = TextBox1.Text
        catalog.description = TextBox2.Text
        catalog.footer = TextBox3.Text
        catalog.buttonText = TextBox4.Text
        Dim category As WAcatalog
        Dim Result As New List(Of WAcatalog)
        For Each cat As CatalogCategory In FlowLayoutPanel1.Controls
            category = New WAcatalog
            category.title = cat.CategoryName
            category.rows = cat.GetItems
            Result.Add(category)
        Next
        catalog.sections = Result
        Dim dg As New SaveFileDialog
        dg.Filter = "*.Catalog|*.catalog"
        If dg.ShowDialog = DialogResult.OK Then
            Try
                IO.File.WriteAllText(dg.FileName, Newtonsoft.Json.JsonConvert.SerializeObject(catalog))
            Catch ex As Exception
                MsgBox(ex.Message, vbCritical, Application.ProductName)
            End Try

        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        CatalogFileName = ""
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim dg As New OpenFileDialog
        dg.Filter = "*.catalog|*.catalog"
        If dg.ShowDialog = DialogResult.OK Then
            Try
                Catalog = Newtonsoft.Json.JsonConvert.DeserializeObject(Of WACatalogDetails)(IO.File.ReadAllText(dg.FileName))
                TextBox1.Text = Catalog.title
                TextBox2.Text = Catalog.description
                TextBox3.Text = Catalog.footer
                TextBox4.Text = Catalog.buttonText
                For Each cat In Catalog.sections
                    Dim Category As New CatalogCategory
                    Category.CategoryName = cat.title
                    Category.LoadRow(cat.rows)
                    FlowLayoutPanel1.Controls.Add(Category)
                Next

            Catch ex As Exception
                MsgBox(ex.Message, vbCritical, Application.ProductName)
            End Try

        End If
    End Sub

    Private Sub FrmCatalogBuilder_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim Category As New CatalogCategory
        FlowLayoutPanel1.Controls.Add(Category)
    End Sub
End Class


