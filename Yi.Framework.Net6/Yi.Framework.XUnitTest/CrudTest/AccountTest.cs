using Yi.Framework.Common.Const;
using Yi.Framework.Core;
using Yi.Framework.Interface;

namespace Yi.Framework.XUnitTest
{
    public class AccountTest
    {
        private IUserService _iUserService;
        public AccountTest(IUserService iUserService) =>
            (_iUserService) =
            (iUserService);

    }
}