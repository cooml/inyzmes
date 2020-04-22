

namespace LinuxEpoll
{
  /// <summary>
  /// 包含几个常用的类型，如需要其他在此类中添加
  /// </summary>
  public class DomainType
  {
    /// <summary> /// 未定义类型
    /// </summary>
    public const int AF_UNSPEC = 0;

    /// <summary>/// local to host (pipes, portals) */ 本地？如pipe ？，类似命名管道
    /// </summary>
    public const int AF_LOCAL = 1;
    /// <summary>/// 标准AF_LOCAL的名称
    /// </summary>
    public const int AF_UNIX = 1;
    /// <summary> ///  internetwork: UDP, TCP, etc. */ internet，一般都用这个，ipv4
    /// </summary>
    public const int AF_INET = 2;

    /// <summary>///  /* IPv6 */
    /// </summary>
    public const int AF_INET6 = 28;

    /// <summary> /// Internal Routing Protocol
    /// </summary>
    public const int AF_ROUTE = 17;






    // #define AF_UNSPEC       0               /* unspecified */
    //#if __BSD_VISIBLE
    //#define AF_LOCAL        AF_UNIX         /* local to host (pipes, portals) */ 本地？如pipe ？
    //#endif
    //#define AF_UNIX         1               /* standardized name for AF_LOCAL */
    //#define AF_INET         2               /* internetwork: UDP, TCP, etc. */ internet？
    //#if __BSD_VISIBLE
    //#define AF_IMPLINK      3               /* arpanet imp addresses */
    //#define AF_PUP          4               /* pup protocols: e.g. BSP */
    //#define AF_CHAOS        5               /* mit CHAOS protocols */
    //#define AF_NETBIOS      6               /* SMB protocols */
    //#define AF_ISO          7               /* ISO protocols */
    //#define AF_OSI          AF_ISO
    //#define AF_ECMA         8               /* European computer manufacturers */
    //#define AF_DATAKIT      9               /* datakit protocols */
    //#define AF_CCITT        10              /* CCITT protocols, X.25 etc */
    //#define AF_SNA          11              /* IBM SNA */
    //#define AF_DECnet       12              /* DECnet */
    //#define AF_DLI          13              /* DEC Direct data link interface */
    //#define AF_LAT          14              /* LAT */
    //#define AF_HYLINK       15              /* NSC Hyperchannel */
    //#define AF_APPLETALK    16              /* Apple Talk */
    //#define AF_ROUTE        17              /* Internal Routing Protocol */
    //#define AF_LINK         18              /* Link layer interface */
    //#define pseudo_AF_XTP   19              /* eXpress Transfer Protocol (no AF) */
    //#define AF_COIP         20              /* connection-oriented IP, aka ST II */
    //#define AF_CNT          21              /* Computer Network Technology */
    //#define pseudo_AF_RTIP  22              /* Help Identify RTIP packets */
    //#define AF_IPX          23              /* Novell Internet Protocol */
    //#define AF_SIP          24              /* Simple Internet Protocol */
    //#define pseudo_AF_PIP   25              /* Help Identify PIP packets */
    //#define AF_ISDN         26              /* Integrated Services Digital Network*/
    //#define AF_E164         AF_ISDN         /* CCITT E.164 recommendation */
    //#define pseudo_AF_KEY   27              /* Internal key-management function */
    //#endif
    //#define AF_INET6        28              /* IPv6 */
    //#if __BSD_VISIBLE
    //#define AF_NATM         29              /* native ATM access */
    //#define AF_ATM          30              /* ATM */
    //#define pseudo_AF_HDRCMPLT 31           /* Used by BPF to not rewrite headers
    //                                         * in interface output routine
    //                                         */
    //#define AF_NETGRAPH     32              /* Netgraph sockets */
    //#define AF_SLOW         33              /* 802.3ad slow protocol */
    //#define AF_SCLUSTER     34              /* Sitara cluster protocol */
    //#define AF_ARP          35
    //#define AF_BLUETOOTH    36              /* Bluetooth sockets */
    //#define AF_IEEE80211    37              /* IEEE 802.11 protocol */
    //#define AF_INET_SDP     40              /* OFED Socket Direct Protocol ipv4 */
    //#define AF_INET6_SDP    42              /* OFED Socket Direct Protocol ipv6 */
    //#define AF_MAX          42
    ///*
    // * When allocating a new AF_ constant, please only allocate
    // * even numbered constants for FreeBSD until 134 as odd numbered AF_
    // * constants 39-133 are now reserved for vendors.
    // */
    //#define AF_VENDOR00 39
    //#define AF_VENDOR01 41
    //#define AF_VENDOR02 43
    //#define AF_VENDOR03 45
    //#define AF_VENDOR04 47
    //#define AF_VENDOR05 49
    //#define AF_VENDOR06 51
    //#define AF_VENDOR07 53
    //#define AF_VENDOR08 55
    //#define AF_VENDOR09 57
    //#define AF_VENDOR10 59
    //#define AF_VENDOR11 61
    //#define AF_VENDOR12 63
    //#define AF_VENDOR13 65
    //#define AF_VENDOR14 67
    //#define AF_VENDOR15 69
    //#define AF_VENDOR16 71
    //#define AF_VENDOR17 73
    //#define AF_VENDOR18 75
    //#define AF_VENDOR19 77
    //#define AF_VENDOR20 79
    //#define AF_VENDOR21 81
    //#define AF_VENDOR22 83
    //#define AF_VENDOR23 85
    //#define AF_VENDOR24 87
    //#define AF_VENDOR25 89
    //#define AF_VENDOR26 91
    //#define AF_VENDOR27 93
    //#define AF_VENDOR28 95
    //#define AF_VENDOR29 97
    //#define AF_VENDOR30 99
    //#define AF_VENDOR31 101
    //#define AF_VENDOR32 103
    //#define AF_VENDOR33 105
    //#define AF_VENDOR34 107
    //#define AF_VENDOR35 109
    //#define AF_VENDOR36 111
    //#define AF_VENDOR37 113
    //#define AF_VENDOR38 115
    //#define AF_VENDOR39 117
    //#define AF_VENDOR40 119
    //#define AF_VENDOR41 121
    //#define AF_VENDOR42 123
    //#define AF_VENDOR43 125
    //#define AF_VENDOR44 127
    //#define AF_VENDOR45 129
    //#define AF_VENDOR46 131
    //#define AF_VENDOR47 133




  }
}
