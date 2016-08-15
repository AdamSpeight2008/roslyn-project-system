' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

' All help keywords for anything inside this assembly should be defined here,
'   so that it is easier for UE to find them.
'


Option Explicit On
Option Strict On
Option Compare Binary



'****************************************************
'*****  Property Page Help IDs
'****************************************************

Namespace Microsoft.VisualStudio.Editors.PropertyPages

    Friend NotInheritable Class HelpKeywords
        Friend Const VBProjPropVersion = "vb.ProjectPropertiesVersion"
        Friend Const VBProjPropSigning = "vb.ProjectPropertiesSigning"
        Friend Const VBProjPropAdvancedCompile = "vb.ProjectPropertiesAdvancedCompile"
        Friend Const VBProjPropAdvancedServices = "vb.ProjectPropertiesAdvancedServices"
        Friend Const VBProjPropApplication = "vb.ProjectPropertiesApplication"
        Friend Const VBProjPropApplicationWPF = "vb.ProjectPropertiesApplicationWPF"
        Friend Const VBProjPropAssemblyInfo = "vb.ProjectPropertiesAssemblyInfo"
        Friend Const VBProjPropCompile = "vb.ProjectPropertiesCompile"
        Friend Const VBProjPropDatabase = "vdt.ProjectProperties.SQL.Database"
        Friend Const VBProjPropDebug = "vb.ProjectPropertiesDebug"
        Friend Const VBProjPropDeploy = "vdt.ProjectProperties.SQL.Deploy"
        Friend Const VBProjPropReference = "vb.ProjectPropertiesReference"
        Friend Const VBProjPropReferencePaths = "vb.ProjectPropertiesReferencePaths"
        Friend Const VBProjPropSettingsLogin = "vb.ProjectPropertiesSettingsLogin"
        Friend Const VBProjPropImports = "vb.ProjectPropertiesImports"
        Friend Const VBProjPropUnusedReference = "vb.ProjectPropertiesUnusedReference"
        Friend Const VBProjPropSecurity = "vb.ProjectPropertiesSecurity"
        Friend Const VBProjPropSecurityLink = "vb.ProjectPropertiesSecurity.HowTo"
        Friend Const VBProjPropServices = "vb.ProjectPropertiesServices"
        Friend Const VBProjPropXBAPSecurity = "vb.XBAPProjectPropertiesSecurity"
        Friend Const VBProjPropXBAPSecurityLink = "vb.XBAPProjectPropertiesSecurity.HowTo"
        Friend Const VBProjPropXBAPAllowPartiallyTrustedCallers = "System.Security.AllowPartiallyTrustedCallersAttribute"
        Friend Const VBProjPropSigningNewCertDialog = "vb.ProjectPropertiesSigning.PfxPasswordDialog"
        Friend Const VBProjPropSigningPasswordNeededDialog = "vb.ProjectPropertiesSigning.PasswordNeededDialog"
        Friend Const VBProjPropSigningChangePasswordDialog = "vb.ProjectPropertiesSigning.ChangePasswordDialog"
        Friend Const VBProjPropBuildEvents = "vb.ProjectPropertiesBuildEvents"
        Friend Const VBProjPropBuildEventsBuilder = "vb.ProjectPropertiesBuildEventsBuilder"


        Friend Const CSProjPropApplication = "cs.ProjectPropertiesApplication"
        Friend Const CSProjPropBuild = "cs.ProjectPropertiesBuild"
        Friend Const CSProjPropBuildEvents = "cs.ProjectPropertiesBuildEvents"
        Friend Const CSProjPropBuildEventsBuilder = "cs.ProjectPropertiesBuildEventsBuilder"
        Friend Const CSProjPropReference = "cs.ProjectPropertiesReferencePaths"
        Friend Const CSProjPropReferencePaths = "cs.ProjectPropertiesReferencePaths"
        Friend Const CSProjPropAdvancedCompile = "cs.AdvancedBuildSettings"

        Friend Const JSProjPropApplication = "js.ProjectPropertiesApplication"
        Friend Const JSProjPropBuild = "js.ProjectPropertiesBuild"
        Friend Const JSProjPropAdvancedCompile = "js.AdvancedBuildSettings"
        Friend Const JSProjPropReferencePaths = "js.ProjectPropertiesReferencePaths"


        Friend Const VBProjPropWPFApp_CantOpenOrCreateAppXaml = "vb.ProjProp.WPFApp.CantOpenOrCreateAppXaml"
        Friend Const VBProjPropWPFApp_AppXamlOpenInUnsupportedEditor = "vb.ProjProp.WPFApp.AppXamlOpenInUnsupportedEditor"
        Friend Const VBProjPropWPFApp_CouldntCreateApplicationEventsFile = "vb.ProjProp.WPFApp.CouldntCreateApplicationEventsFile"
    End Class

End Namespace



'****************************************************
'*****  Resource Editor Help IDs
'****************************************************

Namespace Microsoft.VisualStudio.Editors.ResourceEditor

    Friend NotInheritable Class HelpIDs

        'General errors
        Public Const Err_CantFindResourceFile = "msvse_resedit.Err.CantFindResourceFile"
        Public Const Err_LoadingResource = "msvse_resedit.Err.LoadingResource"
        Public Const Err_NameBlank = "msvse_resedit.Err.NameBlank"
        Public Const Err_InvalidName = "msvse_resedit.Err.InvalidName"
        Public Const Err_DuplicateName = "msvse_resedit.Err.DuplicateName"
        Public Const Err_UnexpectedResourceType = "msvse_resedit.Err.UnexpectedResourceType"
        Public Const Err_CantCreateNewResource = "msvse_resedit.Err.CantCreateNewResource"
        Public Const Err_CantPlay = "msvse_resedit.Err.CantPlay"
        Public Const Err_CantConvertFromString = "msvse_resedit.Err.CantConvertFromString"
        Public Const Err_EditFormResx = "msvse_resedit.Err.EditFormResx"
        Public Const Err_CantAddFileToDeviceProject = "msvse_resedit.Err.CantAddFileToDeviceProject"
        Public Const Err_TypeIsNotSupported = "msvse_resedit.Err.TypeIsNotSupported"
        Public Const Err_CantSaveBadResouceItem = "msvse_resedit.Err.CantSaveBadResouceItem "
        Public Const Err_MaxFilesLimitation = "msvse_resedit.Err.MaxFilesLimitation"

        'Task list errors
        Public Const Task_BadLink = "msvse_resedit.tasklist.BadLink"
        Public Const Task_CantInstantiate = "msvse_resedit.tasklist.CantInstantiate"
        Public Const Task_NonrecommendedName = "msvse_resedit.tasklist.NonrecommendedName"
        Public Const Task_CantChangeCustomToolOrNamespace = "msvse_resedit.tasklist.CantChangeCustomToolOrNamespace"


        'Dialogs
        Public Const Dlg_OpenEmbedded = "msvse_resedit.dlg.OpenEmbedded"
        Public Const Dlg_QueryName = "msvse_resedit.dlg.QueryName"
        Public Const Dlg_OpenFileWarning = "msvse_resedit.dlg.OpenFileWarning"
    End Class

End Namespace



'****************************************************
'*****  Settings Designer Help IDs
'****************************************************

Namespace Microsoft.VisualStudio.Editors.SettingsDesigner

    Friend NotInheritable Class HelpIDs

        'General errors
        Public Const Err_LoadingSettingsFile = "msvse_settingsdesigner.Err.LoadingSettingsFile"
        Public Const Err_LoadingAppConfigFile = "msvse_settingsdesigner.Err.LoadingAppConfigFile"
        Public Const Err_SavingAppConfigFile = "msvse_settingsdesigner.Err.SavingAppConfigFile"
        Public Const Err_NameBlank = "msvse_settingsdesigner.Err.NameBlank"
        Public Const Err_InvalidName = "msvse_settingsdesigner.Err.InvalidName"
        Public Const Err_DuplicateName = "msvse_settingsdesigner.Err.DuplicateName"
        Public Const Err_FormatValue = "msvse_settingsdesigner.Err.FormatValue"
        Public Const Err_ViewCode = "msvse_settingsdesigner.Err.ViewCode"

        ' Synchronize user config
        Public Const Err_SynchronizeUserConfig = "msvse_settingsdesigner.SynchronizeUserConfig"

        'Dialogs
        Public Const Dlg_PickType = "msvse_settingsdesigner.dlg.PickType"

        ' Help keyword for description link in settings designer
        Public Const SettingsDesignerDescription = "ApplicationSettingsOverview"


        'My.Settings help keyword (generated into the .settings.designer.vb file in VB)
        Public Const MySettingsHelpKeyword = "My.Settings"


        ' Can't create this guy!
        Private Sub New()
        End Sub
    End Class

End Namespace



'****************************************************
'*****  My Extensibility Design-Time Tools HelpIDs
'****************************************************
Namespace Microsoft.VisualStudio.Editors.MyExtensibility
    Friend NotInheritable Class HelpIDs
        Public Const Dlg_AddMyNamespaceExtensions = "vb.AddingMyExtensions"
        Public Const VBProjPropMyExtensions = "vb.ProjectPropertiesMyExtensions"

        Private Sub New()
        End Sub
    End Class
End Namespace
