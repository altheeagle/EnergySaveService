Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.ServiceProcess

<RunInstaller(True)> Public Class Installer1
    Inherits System.Configuration.Install.Installer
    ' Die Variable ServiceName muss der ServiceName- 
    ' Eigenschaft Ihres Dienstes entsprechen: 
    Private ServiceName As String = "Windows Dienst"
    ' Klartextbeschreibung des Dienstes: 
    Private ServiceDescription = "Windows Dienst"
#Region " Component Designer generated code "

    Public Sub New()
        ' Installer-Objekte erzeugen: 
        Dim ProcessInstaller As ServiceProcessInstaller _
         = New ServiceProcessInstaller
        Dim ServiceInstaller As ServiceInstaller _
         = New ServiceInstaller
        ' Der Dienst soll unter dem lokalen System-Account laufen: 
        ProcessInstaller.Account = ServiceAccount.LocalSystem
        ' Der Dienst soll manuell gestartet werden: 
        ServiceInstaller.StartType = ServiceStartMode.Manual
        ' Die ServiceName-Eigenschaft muss der ServiceName- 
        ' Eigenschaft Ihres Dienstes entsprechen: 
        ServiceInstaller.ServiceName = ServiceName
        ' Die Installer-Objekte der Installers-Collection hinzufügen: 
        Installers.Add(ServiceInstaller)
        Installers.Add(ProcessInstaller)
    End Sub

    ' Workaround zur Hinterlegung der Dienstbeschreibung 
    Public Overrides Sub Install(ByVal stateSaver As _
   System.Collections.IDictionary)
        ' Der Klasse ServiceInstaller des .NET Frameworks 1.0 fehlt  
        ' eine Eigenschaft, um die Klartextbeschreibung eines  
        ' Dienstes in der Registry zu hinterlegen. Dieser Workaround 
        ' trägt die Klartextbeschreibung nach Installation des 
        ' Dienstes in die Registry unter dem Zweig 
        ' HKEY_LOCAL_MACHINE\System\CurrentControlSet\... 
        ' ...Services\[Dienstname]\ 
        ' als Wert des Schlüssels "Description" ein. 
        ' Ein Löschen bei der Deinstallation des Dienstes ist nicht 
        ' notwendig, da die Deinstallation den gesamten Schlüssel 
        ' [Dienstname] mit allen enthaltenen Werten löscht. 
        Dim RegKey As Microsoft.Win32.RegistryKey
        ' Installation durchführen 
        MyBase.Install(stateSaver)
        ' Danach die Klartextbeschreibung in der Registry ablegen 
        RegKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey( _
         "System").OpenSubKey( _
        "CurrentControlSet").OpenSubKey( _
          "Services").OpenSubKey( _
         ServiceName, True)
        RegKey.SetValue("Description", ServiceDescription)
        RegKey.Close()

    End Sub

    'Installer overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

End Class
