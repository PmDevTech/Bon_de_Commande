Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.UserSkins
Imports DevExpress.Skins
Imports DevExpress.XtraSplashScreen

Namespace ClearProject
    Friend NotInheritable Class Program
        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        Private Sub New()
        End Sub
        <STAThread()> _
        Shared Sub Main()
            'SplashScreenManager.ShowForm(GetType(SplashScreen1), True, False)

            'BonusSkins.Register()
            SkinManager.EnableFormSkins()

            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New CodeAcces())
        End Sub
    End Class
End Namespace
