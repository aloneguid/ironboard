// Guids.cs
// MUST match guids.h

using System;

namespace IronBoard.Vsix
{
    static class GuidList
    {
        public const string guidIronBoard_VsixPkgString = "e273fbc4-5fd5-44ef-87de-2872303dc3ff";
        public const string guidIronBoard_VsixCmdSetString = "9dbc08b0-502a-4d31-a58c-bc889cff0f1a";

        public static readonly Guid guidIronBoard_VsixCmdSet = new Guid(guidIronBoard_VsixCmdSetString);
    };
}