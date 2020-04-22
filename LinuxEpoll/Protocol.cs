using System;
using System.Collections.Generic;
using System.Text;

namespace LinuxEpoll
{
  /// <summary>
  /// 包含几个常用的类型，如需要其他在此类中添加
  /// </summary>
  public class Protocol
  {
    /// <summary>
    /// Dummy protocol for TCP.，TCP虚拟协议。,自动适应
    /// </summary>
    public const int IPPROTO_IP = 0;
    /// <summary>
    ///  Internet Control Message Protocol.，该协议简写为ICMP，即Internet控制报文协议，著名的ping命令就是利用该协议来实现的。
    /// </summary>
    public const int IPPROTO_ICMP = 1;
    /// <summary>
    ///  Transmission Control Protocol.， TCP协议，即传输层控制协议。
    /// </summary>
    public const int IPPROTO_TCP = 6;
    /// <summary>
    ///   User Datagram Protocol.    UDP协议，即用户数据报协议。
    /// </summary>
    public const int IPPROTO_UDP = 17;
    /// <summary>
    /// Multicast Transport Protocol，组播传输协议
    /// </summary>
    public const int IPPROTO_MTP = 92;



    //        IPPROTO_IP = 0,       /* Dummy protocol for TCP. */
    //#define IPPROTO_IP        IPPROTO_IP
    //    IPPROTO_HOPOPTS = 0,  /* IPv6 Hop-by-Hop options. */
    //#define IPPROTO_HOPOPTS        IPPROTO_HOPOPTS
    //    IPPROTO_ICMP = 1,     /* Internet Control Message Protocol. */
    //#define IPPROTO_ICMP        IPPROTO_ICMP
    //    IPPROTO_IGMP = 2,     /* Internet Group Management Protocol. */
    //#define IPPROTO_IGMP        IPPROTO_IGMP
    //    IPPROTO_IPIP = 4,     /* IPIP tunnels (older KA9Q tunnels use 94). */
    //#define IPPROTO_IPIP        IPPROTO_IPIP
    //    IPPROTO_TCP = 6,      /* Transmission Control Protocol. */
    //#define IPPROTO_TCP        IPPROTO_TCP
    //    IPPROTO_EGP = 8,      /* Exterior Gateway Protocol. */
    //#define IPPROTO_EGP        IPPROTO_EGP
    //    IPPROTO_PUP = 12,     /* PUP protocol. */
    //#define IPPROTO_PUP        IPPROTO_PUP
    //    IPPROTO_UDP = 17,     /* User Datagram Protocol. */
    //#define IPPROTO_UDP        IPPROTO_UDP
    //    IPPROTO_IDP = 22,     /* XNS IDP protocol. */
    //#define IPPROTO_IDP        IPPROTO_IDP
    //    IPPROTO_TP = 29,      /* SO Transport Protocol Class 4. */
    //#define IPPROTO_TP        IPPROTO_TP
    //    IPPROTO_DCCP = 33,    /* Datagram Congestion Control Protocol. */
    //#define IPPROTO_DCCP        IPPROTO_DCCP
    //    IPPROTO_IPV6 = 41,    /* IPv6 header. */
    //#define IPPROTO_IPV6        IPPROTO_IPV6
    //    IPPROTO_ROUTING = 43, /* IPv6 routing header. */
    //#define IPPROTO_ROUTING        IPPROTO_ROUTING
    //    IPPROTO_FRAGMENT = 44, /* IPv6 fragmentation header. */
    //#define IPPROTO_FRAGMENT    IPPROTO_FRAGMENT
    //    IPPROTO_RSVP = 46,    /* Reservation Protocol. */
    //#define IPPROTO_RSVP        IPPROTO_RSVP
    //    IPPROTO_GRE = 47,     /* General Routing Encapsulation. */
    //#define IPPROTO_GRE        IPPROTO_GRE
    //    IPPROTO_ESP = 50,     /* encapsulating security payload. */
    //#define IPPROTO_ESP        IPPROTO_ESP
    //    IPPROTO_AH = 51,      /* authentication header. */
    //#define IPPROTO_AH        IPPROTO_AH
    //    IPPROTO_ICMPV6 = 58,  /* ICMPv6. */
    //#define IPPROTO_ICMPV6        IPPROTO_ICMPV6
    //    IPPROTO_NONE = 59,    /* IPv6 no next header. */
    //#define IPPROTO_NONE        IPPROTO_NONE
    //    IPPROTO_DSTOPTS = 60, /* IPv6 destination options. */
    //#define IPPROTO_DSTOPTS        IPPROTO_DSTOPTS
    //    IPPROTO_MTP = 92,     /* Multicast Transport Protocol. */
    //#define IPPROTO_MTP        IPPROTO_MTP
    //    IPPROTO_ENCAP = 98,   /* Encapsulation Header. */
    //#define IPPROTO_ENCAP        IPPROTO_ENCAP
    //    IPPROTO_PIM = 103,    /* Protocol Independent Multicast. */
    //#define IPPROTO_PIM        IPPROTO_PIM
    //    IPPROTO_COMP = 108,   /* Compression Header Protocol. */
    //#define IPPROTO_COMP        IPPROTO_COMP
    //    IPPROTO_SCTP = 132,   /* Stream Control Transmission Protocol. */
    //#define IPPROTO_SCTP        IPPROTO_SCTP
    //    IPPROTO_UDPLITE = 136, /* UDP-Lite protocol. */
    //#define IPPROTO_UDPLITE        IPPROTO_UDPLITE
    //    IPPROTO_RAW = 255,     /* Raw IP packets. */
    //#define IPPROTO_RAW        IPPROTO_RAW
    //    IPPROTO_MAX
    //    };
    //    很显然，上面的代码定义了所以基于IP协议的一些协议的枚举。
    //其中为我们大多数人熟知的协议如下：
    //1.    IPPROTO_ICMP = 1,	   /* Internet Control Message Protocol.  */
    //      该协议简写为ICMP，即Internet控制报文协议，著名的ping命令就是利用该协议来实现的。
    //2.    IPPROTO_TCP = 6,	   /* Transmission Control Protocol.  */
    //       TCP协议，即传输层控制协议。
    //3.    IPPROTO_UDP = 17,	   /* User Datagram Protocol.  */
    //       UDP协议，即用户数据报协议。
    //4.    IPPROTO_MTP = 92,	   /* Multicast Transport Protocol.  */
    //       多播传输协议。
    //还有它们对应的IPv6的协议。
    ///* Type to represent a port. */
    //typedef uint16_t in_port_t;
    ///* Standard well-known ports. */
    //enum
    //{
    //        IPPORT_ECHO = 7,           /* Echo service. */
    //        IPPORT_DISCARD = 9,        /* Discard transmissions service. */
    //        IPPORT_SYSTAT = 11,        /* System status service. */
    //        IPPORT_DAYTIME = 13,       /* Time of day service. */
    //        IPPORT_NETSTAT = 15,       /* Network status service. */
    //        IPPORT_FTP = 21,           /* File Transfer Protocol. */
    //        IPPORT_TELNET = 23,        /* Telnet protocol. */
    //        IPPORT_SMTP = 25,          /* Simple Mail Transfer Protocol. */
    //        IPPORT_TIMESERVER = 37,    /* Timeserver service. */
    //        IPPORT_NAMESERVER = 42,    /* Domain Name Service. */
    //        IPPORT_WHOIS = 43,         /* Internet Whois service. */
    //        IPPORT_MTP = 57,
    //        IPPORT_TFTP = 69,          /* Trivial File Transfer Protocol. */
    //        IPPORT_RJE = 77,
    //        IPPORT_FINGER = 79,        /* Finger service. */
    //        IPPORT_TTYLINK = 87,
    //        IPPORT_SUPDUP = 95,        /* SUPDUP protocol. */
    //        IPPORT_EXECSERVER = 512,    /* execd service. */
    //        IPPORT_LOGINSERVER = 513,   /* rlogind service. */
    //        IPPORT_CMDSERVER = 514,
    //        IPPORT_EFSSERVER = 520,
    //        /* UDP ports. */
    //        IPPORT_BIFFUDP = 512,
    //        IPPORT_WHOSERVER = 513,
    //        IPPORT_ROUTESERVER = 520,
    //        /* Ports less than this value are reserved for privileged processes. */
    //        IPPORT_RESERVED = 1024,
    //        /* Ports greater this value are reserved for (non-privileged) servers. */
    //        IPPORT_USERRESERVED = 5000
    //    };
    //    typedef uint16_t in_port_t;定义了端口为一个16位的无符号整数。
    //接下来的enum定义了一些系统常用的端口号。
    //    IPPORT_NETSTAT = 15,	       /* Network status service.  */  提供netstat命令行服务的端口号
    //    IPPORT_FTP = 21,            /* File Transfer Protocol.  */  FTP使用的端口号
    //    IPPORT_TELNET = 23,               /* Telnet protocol.  */  提供telent命令行服务的端口号
    //    IPPORT_SMTP = 25,           /* Simple Mail Transfer Protocol.  */ 简单邮件协议的端口号
    //    IPPORT_NAMESERVER = 42,     /* Domain Name Service.  */  DNS的端口号
    //    /* Ports less than this value are reserved for privileged processes.  */
    //    IPPORT_RESERVED = 1024,
    //所以小于1024的端口号是系统预用的端口号。
    ///* Internet address. */
    //typedef uint32_t in_addr_t;
    //struct in_addr
    //    {
    //        in_addr_t s_addr;
    //    };
    //    上面的结构定义了IP地址，为一个32位的无符号整数。
    ///* Definitions of the bits in an Internet address integer.
    //   On subnets, host and network parts are found according to
    //   the subnet mask, not these masks. 
    //*/
    //#define    IN_CLASSA(a)        ((((in_addr_t)(a)) & 0x80000000) == 0)
    //#define    IN_CLASSA_NET        0xff000000
    //#define    IN_CLASSA_NSHIFT    24
    //#define    IN_CLASSA_HOST        (0xffffffff & ~IN_CLASSA_NET)
    //#define    IN_CLASSA_MAX        128
    //#define    IN_CLASSB(a)        ((((in_addr_t)(a)) & 0xc0000000) == 0x80000000)
    //#define    IN_CLASSB_NET        0xffff0000
    //#define    IN_CLASSB_NSHIFT    16
    //#define    IN_CLASSB_HOST        (0xffffffff & ~IN_CLASSB_NET)
    //#define    IN_CLASSB_MAX        65536
    //#define    IN_CLASSC(a)        ((((in_addr_t)(a)) & 0xe0000000) == 0xc0000000)
    //#define    IN_CLASSC_NET        0xffffff00
    //#define    IN_CLASSC_NSHIFT    8
    //#define    IN_CLASSC_HOST        (0xffffffff & ~IN_CLASSC_NET)
    //#define    IN_CLASSD(a)        ((((in_addr_t)(a)) & 0xf0000000) == 0xe0000000)
    //#define    IN_MULTICAST(a)        IN_CLASSD(a)
    //#define    IN_EXPERIMENTAL(a)    ((((in_addr_t)(a)) & 0xe0000000) == 0xe0000000)
    //#define    IN_BADCLASS(a)        ((((in_addr_t)(a)) & 0xf0000000) == 0xf0000000)
    ///* Address to accept any incoming messages. */
    //#define    INADDR_ANY        ((in_addr_t) 0x00000000)
    ///* Address to send to all hosts. */
    //#define    INADDR_BROADCAST    ((in_addr_t) 0xffffffff)
    ///* Address indicating an error return. */
    //#define    INADDR_NONE        ((in_addr_t) 0xffffffff)
    ///* Network number for local host loopback. */
    //#define    IN_LOOPBACKNET        127
    ///* Address to loopback in software to local host. */
    //#ifndef INADDR_LOOPBACK
    //# define INADDR_LOOPBACK    ((in_addr_t) 0x7f000001) /* Inet 127.0.0.1. */
    //#endif
    ///* Defines for Multicast INADDR. */
    //#define INADDR_UNSPEC_GROUP    ((in_addr_t) 0xe0000000) /* 224.0.0.0 */
    //#define INADDR_ALLHOSTS_GROUP    ((in_addr_t) 0xe0000001) /* 224.0.0.1 */
    //#define INADDR_ALLRTRS_GROUP ((in_addr_t) 0xe0000002) /* 224.0.0.2 */
    //#define INADDR_MAX_LOCAL_GROUP ((in_addr_t) 0xe00000ff) /* 224.0.0.255 */
    //#define	IN_CLASSA(a)            ((((in_addr_t)(a)) & 0x80000000) == 0)
    //#define	IN_CLASSB(a)	 ((((in_addr_t)(a)) & 0xc0000000) == 0x80000000)
    //#define	IN_CLASSC(a)	 ((((in_addr_t)(a)) & 0xe0000000) == 0xc0000000)
    //#define	IN_CLASSD(a)	 ((((in_addr_t)(a)) & 0xf0000000) == 0xe0000000)
    //#define	IN_EXPERIMENTAL(a)	((((in_addr_t)(a)) & 0xe0000000) == 0xe0000000)
    //#define	IN_BADCLASS(a)	 ((((in_addr_t)(a)) & 0xf0000000) == 0xf0000000)
    //该宏用于判断一个IP地址是否是一个A类地址，因为我们知道：
    //A类地址的最高位为：0
    //B类地址的最高位为：10
    //C类地址的最高位为：110
    //D类地址的最高位为：1110
    //这几个宏分别：
    //提起IP地址的最高1位，如果最高位是0，则是A类IP地址
    //提起IP地址的最高2位，如果最高2位是10，则是B类IP地址
    //提起IP地址的最高3位，如果最高3位是110，则是C类IP地址
    //提起IP地址的最高4位，如果最高4位是1110，则是D类IP地址
    //#define	IN_CLASSA_NET	 0xff000000
    //用于提起A类IP地址的网络地址部分。
    //#define	IN_CLASSA_NSHIFT	24
    //定义了A类IP地址的网络地址部分开始的偏移为24.（24--31位）
    //#define	IN_CLASSA_HOST	 (0xffffffff & ~IN_CLASSA_NET)
    //用于提起A类IP地址的主机地址部分。
    //#define	IN_CLASSA_MAX	 128
    //定义了A类IP地址允许有128个子网（2的7次方等于128）。
    //其他关于B、C、D类地址的相关定义类似。
    ///* Address to accept any incoming messages.  */
    //#define	INADDR_ANY	 ((in_addr_t) 0x00000000)
    //该宏定义的IP地址可以接收所有发送过来的数据包。
    ///* Address to send to all hosts.  */
    //#define	INADDR_BROADCAST	((in_addr_t) 0xffffffff)
    //该宏定义了“有限广播地址”或者称为“本地网广播地址”，它用于在本地子网中进行广播。因为它的32位全部为1，所以它不可能去表示一个IP地址的网络地址部分，所以它只能用于本地子网进行广播。我们还有另外一种广播形式成为“定向广播”，它由某个Internate子网的网络部分加上全1的主机地址组成，可以用于定向向该Internate子网中的所有的主机进行广播，因为它可以指定向某个子网进行广播，所以叫“定向广播”。
    ///* Network number for local host loopback.  */
    //#define	IN_LOOPBACKNET	 127
    //定义了本地回网的网络地址部分。
    ///* Address to loopback in software to local host.  */
    //#ifndef INADDR_LOOPBACK
    //# define INADDR_LOOPBACK	((in_addr_t) 0x7f000001) /* Inet 127.0.0.1.  */
    //#endif
    //定义了本地回网的IP地址。
    ///* Defines for Multicast INADDR.  */
    //#define INADDR_UNSPEC_GROUP	((in_addr_t) 0xe0000000) /* 224.0.0.0 */
    //#define INADDR_ALLHOSTS_GROUP	((in_addr_t) 0xe0000001) /* 224.0.0.1 */
    //#define INADDR_ALLRTRS_GROUP    ((in_addr_t) 0xe0000002) /* 224.0.0.2 */
    //#define INADDR_MAX_LOCAL_GROUP  ((in_addr_t) 0xe00000ff) /* 224.0.0.255 */
    //这些宏定义了相关的组播地址。
    //#define INET_ADDRSTRLEN 16
    //#define INET6_ADDRSTRLEN 46
    //定义了IPv4的IP地址的长度为16个字节，IPv6的IP地址长度为46字节。
    ///* Structure describing an Internet socket address. */
    //struct sockaddr_in
    //    {
    //    __SOCKADDR_COMMON(sin_);
    //        in_port_t sin_port;             /* Port number. */
    //        struct in_addr sin_addr;        /* Internet address. */
    //    /* Pad to size of `struct sockaddr'. */
    //    unsigned char sin_zero[sizeof(struct sockaddr) -
    //             __SOCKADDR_COMMON_SIZE -
    //             sizeof (in_port_t) -
    //             sizeof (struct in_addr)];
    //};
    //    定义了表示IP地址的结构体。显然该结构体的大小和struct sockaddr的大小一样。
    ///* IPv4 multicast request. */
    //struct ip_mreq
    //    {
    //        /* IP multicast address of group. */
    //        struct in_addr imr_multiaddr;
    //    /* Local IP address of interface. */
    //    struct in_addr imr_interface;
    //};
    //    struct ip_mreq_source
    //    {
    //        /* IP multicast address of group. */
    //        struct in_addr imr_multiaddr;
    //    /* IP address of source. */
    //    struct in_addr imr_interface;
    //    /* IP address of interface. */
    //    struct in_addr imr_sourceaddr;
  }
}
