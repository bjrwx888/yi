using Volo.Abp.Application.Services;

namespace Yi.Framework.GoView.Application.Services
{
    public class TestService : ApplicationService
    {


        public string GetHello()
        {
            return "hello";
        }
    }
}
