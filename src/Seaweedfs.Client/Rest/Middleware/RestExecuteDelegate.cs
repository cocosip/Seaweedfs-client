using System.Threading.Tasks;

namespace Seaweedfs.Client.Rest
{

    /// <summary>Rest接口管道委托
    /// </summary>
    public delegate Task RestExecuteDelegate(RestExecuteContext context);
}
