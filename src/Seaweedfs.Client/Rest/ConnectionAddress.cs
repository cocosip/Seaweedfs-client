namespace Seaweedfs.Client.Rest
{

    /// <summary>连接地址实体
    /// </summary>
    public class ConnectionAddress
    {
        /// <summary>string类型IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>端口号
        /// </summary>
        public int Port { get; set; }


        /// <summary>Ctor
        /// </summary>
        public ConnectionAddress()
        {

        }

        /// <summary>Ctor
        /// </summary>
        public ConnectionAddress(string iPAddress, int port)
        {
            IPAddress = iPAddress;
            Port = port;
        }

        /// <summary>Ctor
        /// </summary>
        public ConnectionAddress(string iPAddressWithPort)
        {
            var array = iPAddressWithPort.Split(':');
            IPAddress = array[0];
            Port = int.Parse(array[1]);
        }


        /// <summary>重写Equals方法
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is ConnectionAddress))
            {
                return false;
            }
            ConnectionAddress connectionAddress = (ConnectionAddress) obj;
            return IPAddress.Equals(connectionAddress.IPAddress) && Port.Equals(connectionAddress.Port);
        }

        /// <summary>重写获取HashCode
        /// </summary>
        public override int GetHashCode()
        {
            return IPAddress.GetHashCode() ^ Port.GetHashCode();
        }

        /// <summary>重写ToString方法
        /// </summary>
        public override string ToString()
        {
            return $"{IPAddress}:{Port}";
        }
    }
}
