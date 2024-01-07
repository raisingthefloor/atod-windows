// Copyright 2022-2024 Raising the Floor - US, Inc.
//
// The R&D leading to these results received funding from the:
// * Rehabilitation Services Administration, US Dept. of Education under
//   grant H421A150006 (APCP)
// * National Institute on Disability, Independent Living, and
//   Rehabilitation Research (NIDILRR)
// * Administration for Independent Living & Dept. of Education under grants
//   H133E080022 (RERC-IT) and H133E130028/90RE5003-01-00 (UIITA-RERC)
// * European Union's Seventh Framework Programme (FP7/2007-2013) grant
//   agreement nos. 289016 (Cloud4all) and 610510 (Prosperity4All)
// * William and Flora Hewlett Foundation
// * Ontario Ministry of Research and Innovation
// * Canadian Foundation for Innovation
// * Adobe Foundation
// * Consumer Electronics Association Foundation

using System;

namespace AToD.Deployment.MSI;

internal class PInvokeUtils
{
    internal struct InstallUiHandlerMessageType
    {
        public ExtendedPInvoke.MessageBoxStyle MessageBoxStyle;
        public ExtendedPInvoke.MessageBoxIconType MessageBoxIconType;
        public ExtendedPInvoke.MessageBoxDefaultButton MessageBoxDefaultButton;
        public ExtendedPInvoke.INSTALLMESSAGE InstallationMessageType;

        public InstallUiHandlerMessageType(ExtendedPInvoke.MessageBoxStyle messageBoxStyle, ExtendedPInvoke.MessageBoxIconType messageBoxIconType, ExtendedPInvoke.MessageBoxDefaultButton messageBoxDefaultButton, ExtendedPInvoke.INSTALLMESSAGE installationMessageType)
        {
            this.MessageBoxStyle = messageBoxStyle;
            this.MessageBoxIconType = messageBoxIconType;
            this.MessageBoxDefaultButton = messageBoxDefaultButton;
            this.InstallationMessageType = installationMessageType;
        }

        public static InstallUiHandlerMessageType FromMessageType(uint iMessageType)
        {
            // NOTE: as of Oct 2022, all message box styles can be masked using 0x00000007
            var messageBoxStyle = (ExtendedPInvoke.MessageBoxStyle)(iMessageType & 0x0000000F);
            //
            // NOTE: as of Oct 2022, all message box icon types can be masked using 0x000000F0 (with MB_USERICON, notably, equaling 0x00000080)
            var messageBoxIconType = (ExtendedPInvoke.MessageBoxIconType)(iMessageType & 0x000000F0);
            //
            // NOTE: as of Oct 2022, all message default buttons can be masked using 0x00000300
            var messageBoxDefaultButton = (ExtendedPInvoke.MessageBoxDefaultButton)(iMessageType & 0x00000F00);
            //
            // NOTE: as of Oct 2022, all installation message types can be masked using 0x1F000000
            var installationMessageType = (ExtendedPInvoke.INSTALLMESSAGE)(iMessageType & 0xFF000000);

            return new InstallUiHandlerMessageType(messageBoxStyle, messageBoxIconType, messageBoxDefaultButton, installationMessageType);
        }
    }
}
