using System.ComponentModel;

namespace FA.JustBlog.Common
{
    public class BaseEnum
    {

        public enum ETagColors
        {
            primary, danger, warning, success, secondary
        }
        public enum EUploadFile
        {
            [Description("Delete")]
            Delete = -1,
            [Description("InActive")]
            InActive = 0,
            [Description("Active")]
            Active = 1,
        }

    }
}

