using System.Xml.Serialization;

namespace XMLdemo.Model;

[XmlRoot(ElementName="link-unit", Namespace="system")]
	public class LinkUnit {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlAttribute(AttributeName="target")]
		public string Target { get; set; } = null!;
	}

	[XmlRoot(ElementName="ethernet-adapter", Namespace="automation.ethernet")]
	public class EthernetAdapter {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlAttribute(AttributeName="address")]
		public string Address { get; set; } = null!;

		[XmlAttribute(AttributeName="network")]
		public string Network { get; set; } = null!;
	}

	[XmlRoot(ElementName="ethernet-adapter-binding", Namespace="automation.ethernet")]
	public class EthernetAdapterBinding {
		[XmlAttribute(AttributeName="adapter")]
		public string Adapter { get; set; } = null!;
	}

	[XmlRoot(ElementName="opcda-server", Namespace="server")]
	public class OpcdaServer {
		[XmlElement(ElementName="ethernet-adapter-binding", Namespace="automation.ethernet")]
		public EthernetAdapterBinding EthernetAdapterBinding { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="opcaeserver-module", Namespace="server")]
	public class OpcaeServerModule {
		[XmlElement(ElementName="ethernet-adapter-binding", Namespace="automation.ethernet")]
		public EthernetAdapterBinding EthernetAdapterBinding { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="hda-server", Namespace="server")]
	public class HdaServer {
		[XmlElement(ElementName="ethernet-adapter-binding", Namespace="automation.ethernet")]
		public EthernetAdapterBinding EthernetAdapterBinding { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="opcua-module", Namespace="server")]
	public class OpcuaModule {
		[XmlElement(ElementName="ethernet-adapter-binding", Namespace="automation.ethernet")]
		public EthernetAdapterBinding EthernetAdapterBinding { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="tcp-server", Namespace="server")]
	public class TcpServer {
		[XmlElement(ElementName="ethernet-adapter-binding", Namespace="automation.ethernet")]
		public EthernetAdapterBinding EthernetAdapterBinding { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlAttribute(AttributeName="history-port")]
		public string HistoryPort { get; set; } = null!;
	}

	[XmlRoot(ElementName="historian-database-link", Namespace="server")]
	public class HistorianDatabaseLink {
		[XmlAttribute(AttributeName="database")]
		public string Database { get; set; } = null!;
	}

	[XmlRoot(ElementName="history-module", Namespace="server")]
	public class HistoryModule {
		[XmlElement(ElementName="historian-database-link", Namespace="server")]
		public HistorianDatabaseLink HistorianDatabaseLink { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlElement(ElementName="postgre-database-link", Namespace="server")]
		public PostgresDatabaseLink PostgresDatabaseLink { get; set; } = null!;

		[XmlAttribute(AttributeName="logging-required")]
		public string LoggingRequired { get; set; } = null!;

		[XmlAttribute(AttributeName="system-log-detail-level")]
		public string SystemLogDetailLevel { get; set; } = null!;
	}

	[XmlRoot(ElementName="opcda-link-map", Namespace="automation.opc")]
	public class OpcdaLinkMap {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlAttribute(AttributeName="file")]
		public string File { get; set; } = null!;
	}

	[XmlRoot(ElementName="init-ref", Namespace="automation.reference")]
	public class InitRef {
		[XmlAttribute(AttributeName="ref")]
		public string Ref { get; set; } = null!;

		[XmlAttribute(AttributeName="target")]
		public string Target { get; set; } = null!;
	}

	[XmlRoot(ElementName="object", Namespace="automation.control")]
	public class Child {
		[XmlElement(ElementName="init-ref", Namespace="automation.reference")]
		public InitRef InitRef { get; set; } = null!;
		
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;
		
		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
		
		[XmlAttribute(AttributeName="original")]
		public string Original { get; set; } = null!;
		
		[XmlAttribute(AttributeName="base-type")]
		public string BaseType { get; set; } = null!;
		
		[XmlAttribute(AttributeName="aspect")]
		public string Aspect { get; set; } = null!;
		
		[XmlElement(ElementName="object", Namespace="automation.control")]
		public List<Child> Children { get; set; } = null!;
		
		[XmlElement(ElementName="attribute", Namespace="system")]
		public List<Attribute> Attribute { get; set; } = null!;
		
		[XmlAttribute(AttributeName="access-level")]
		public string AccessLevel { get; set; } = null!;
		
		[XmlAttribute(AttributeName="access-scope")]
		public string AccessScope { get; set; } = null!;
	}

	[XmlRoot(ElementName="parameter", Namespace="automation.control")]
	public class Parameter {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; } = null!;

		[XmlAttribute(AttributeName="direction")]
		public string Direction { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlElement(ElementName="attribute", Namespace="system")]
		public List<Attribute> Attribute { get; set; } = null!;
	}

	[XmlRoot(ElementName="trigger", Namespace="automation.control")]
	public class Trigger {
		[XmlAttribute(AttributeName="on")]
		public string On { get; set; } = null!;

		[XmlAttribute(AttributeName="cause")]
		public string Cause { get; set; } = null!;
	}

	[XmlRoot(ElementName="property", Namespace="system")]
	public class Property {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlText]
		public string Text { get; set; } = null!;
	}

	[XmlRoot(ElementName="handler", Namespace="automation.control")]
	public class Handler {
		[XmlElement(ElementName="trigger", Namespace="automation.control")]
		public Trigger Trigger { get; set; } = null!;

		[XmlElement(ElementName="property", Namespace="system")]
		public Property Property { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="application-object", Namespace="automation.deployment")]
	public class ApplicationObject {
		[XmlElement(ElementName="opcda-link-map", Namespace="automation.opc")]
		public OpcdaLinkMap OpcdaLinkMap { get; set; } = null!;

		[XmlElement(ElementName="object", Namespace="automation.control")]
		public Child Child { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="attribute", Namespace="system")]
	public class Attribute {
		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; } = null!;

		[XmlAttribute(AttributeName="value")]
		public string Value { get; set; } = null!;

		[XmlElement(ElementName="property", Namespace="system")]
		public Property Property { get; set; } = null!;
	}

	[XmlRoot(ElementName="writevqt-module", Namespace="server")]
	public class WriteVqtModule {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="opcda-client-server-link", Namespace="server")]
	public class OpcdaClientServerLink {
		[XmlAttribute(AttributeName="server")]
		public string Server { get; set; } = null!;
	}

	[XmlRoot(ElementName="opcda-client", Namespace="server")]
	public class OpcdaClient {
		[XmlElement(ElementName="opcda-client-server-link", Namespace="server")]
		public OpcdaClientServerLink OpcdaClientServerLink { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="logics-module", Namespace="server")]
	public class LogicsModule {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlAttribute(AttributeName="logging-required")]
		public string LoggingRequired { get; set; } = null!;
	}

	[XmlRoot(ElementName="postgre-database-link", Namespace="server")]
	public class PostgresDatabaseLink {
		[XmlElement(ElementName="property", Namespace="system")]
		public Property Property { get; set; } = null!;

		[XmlAttribute(AttributeName="database")]
		public string Database { get; set; } = null!;

		[XmlAttribute(AttributeName="dsn")]
		public string Dsn { get; set; } = null!;

		[XmlAttribute(AttributeName="queue")]
		public string Queue { get; set; } = null!;

		[XmlAttribute(AttributeName="user")]
		public string User { get; set; } = null!;
	}

	[XmlRoot(ElementName="io-server", Namespace="server")]
	public class IoServer {
		[XmlElement(ElementName="opcda-server", Namespace="server")]
		public OpcdaServer OpcdaServer { get; set; } = null!;

		[XmlElement(ElementName="opcaeserver-module", Namespace="server")]
		public OpcaeServerModule OpcaeServerModule { get; set; } = null!;

		[XmlElement(ElementName="hda-server", Namespace="server")]
		public HdaServer HdaServer { get; set; } = null!;

		[XmlElement(ElementName="opcua-module", Namespace="server")]
		public OpcuaModule OpcuaModule { get; set; } = null!;

		[XmlElement(ElementName="tcp-server", Namespace="server")]
		public TcpServer TcpServer { get; set; } = null!;

		[XmlElement(ElementName="history-module", Namespace="server")]
		public List<HistoryModule> HistoryModule { get; set; } = null!;

		[XmlElement(ElementName="application-object", Namespace="automation.deployment")]
		public ApplicationObject ApplicationObject { get; set; } = null!;

		[XmlElement(ElementName="attribute", Namespace="system")]
		public Attribute Attribute { get; set; } = null!;

		[XmlElement(ElementName="writevqt-module", Namespace="server")]
		public WriteVqtModule WriteVqtModule { get; set; } = null!;

		[XmlElement(ElementName="opcda-client", Namespace="server")]
		public OpcdaClient OpcdaClient { get; set; } = null!;

		[XmlElement(ElementName="logics-module", Namespace="server")]
		public LogicsModule LogicsModule { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="historian-database", Namespace="server")]
	public class HistorianDatabase {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="historian", Namespace="server")]
	public class Historian {
		[XmlElement(ElementName="historian-database", Namespace="server")]
		public HistorianDatabase HistorianDatabase { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="postgre-database", Namespace="server")]
	public class PostgresDatabase {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="usage")]
		public string Usage { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="postgre", Namespace="server")]
	public class Postgres {
		[XmlElement(ElementName="postgre-database", Namespace="server")]
		public PostgresDatabase PostgresDatabase { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="domain-node", Namespace="automation.deployment")]
	public class DomainNode {
		[XmlElement(ElementName="ethernet-adapter", Namespace="automation.ethernet")]
		public EthernetAdapter EthernetAdapter { get; set; } = null!;

		[XmlElement(ElementName="io-server", Namespace="server")]
		public IoServer IoServer { get; set; } = null!;

		[XmlElement(ElementName="historian", Namespace="server")]
		public Historian Historian { get; set; } = null!;

		[XmlElement(ElementName="postgre", Namespace="server")]
		public List<Postgres> Postgres { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="ethernet-net", Namespace="automation.ethernet")]
	public class EthernetNet {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="opcda-server", Namespace="automation.opc")]
	public class OpcdaServer2 {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="class-id")]
		public string Classid { get; set; } = null!;

		[XmlAttribute(AttributeName="tag-map")]
		public string TagMap { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="external-runtime", Namespace="automation.deployment")]
	public class ExternalRuntime {
		[XmlElement(ElementName="application-object", Namespace="automation.deployment")]
		public ApplicationObject ApplicationObject { get; set; } = null!;

		[XmlElement(ElementName="opcda-server", Namespace="automation.opc")]
		public OpcdaServer2 OpcdaServer2 { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="computer", Namespace="automation.deployment")]
	public class Computer {
		[XmlElement(ElementName="ethernet-adapter", Namespace="automation.ethernet")]
		public EthernetAdapter EthernetAdapter { get; set; } = null!;

		[XmlElement(ElementName="external-runtime", Namespace="automation.deployment")]
		public ExternalRuntime ExternalRuntime { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="domain", Namespace="automation.deployment")]
	public class Domain {
		[XmlElement(ElementName="domain-node", Namespace="automation.deployment")]
		public DomainNode DomainNode { get; set; } = null!;

		[XmlElement(ElementName="ethernet-net", Namespace="automation.ethernet")]
		public EthernetNet EthernetNet { get; set; } = null!;

		[XmlElement(ElementName="computer", Namespace="automation.deployment")]
		public Computer Computer { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlAttribute(AttributeName="address")]
		public string Address { get; set; } = null!;
	}

	[XmlRoot(ElementName="aspect", Namespace="automation")]
	public class Aspect {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="namespace", Namespace="system")]
	public class Namespace {
		[XmlElement(ElementName="aspect", Namespace="automation")]
		public List<Aspect> Aspect { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlElement(ElementName="namespace", Namespace="system")]
		public List<Namespace> NameSpace { get; set; } = null!;

		[XmlElement(ElementName="attribute-type", Namespace="system")]
		public AttributeType AttributeType { get; set; } = null!;
	}

	[XmlRoot(ElementName="type", Namespace="automation.control")]
	public class Type {
		[XmlElement(ElementName="attribute", Namespace="system")]
		public List<Attribute> Attribute { get; set; } = null!;

		[XmlElement(ElementName="parameter", Namespace="automation.control")]
		public List<Parameter> Parameter { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlAttribute(AttributeName="aspect")]
		public string Aspect { get; set; } = null!;

		[XmlElement(ElementName="ref", Namespace="automation.reference")]
		public Ref Ref { get; set; } = null!;

		[XmlElement(ElementName="bind", Namespace="automation.control")]
		public List<Bind> Bind { get; set; } = null!;

		[XmlElement(ElementName="socket", Namespace="automation.control")]
		public Socket Socket { get; set; } = null!;

		[XmlElement(ElementName="handler", Namespace="automation.control")]
		public List<Handler> Handler { get; set; } = null!;

		[XmlAttribute(AttributeName="original")]
		public string Original { get; set; } = null!;
	}

	[XmlRoot(ElementName="ref", Namespace="automation.reference")]
	public class Ref {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;

		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; } = null!;

		[XmlAttribute(AttributeName="const-access")]
		public string ConstAccess { get; set; } = null!;

		[XmlAttribute(AttributeName="aspected")]
		public string Aspected { get; set; } = null!;
	}

	[XmlRoot(ElementName="bind", Namespace="automation.control")]
	public class Bind {
		[XmlElement(ElementName="attribute", Namespace="system")]
		public Attribute Attribute { get; set; } = null!;

		[XmlAttribute(AttributeName="source")]
		public string Source { get; set; } = null!;

		[XmlAttribute(AttributeName="target")]
		public string Target { get; set; } = null!;
	}

	[XmlRoot(ElementName="socket", Namespace="automation.control")]
	public class Socket {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; } = null!;

		[XmlAttribute(AttributeName="direction")]
		public string Direction { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="socket-parameter", Namespace="automation.control")]
	public class SocketParameter {
		[XmlElement(ElementName="attribute", Namespace="system")]
		public Attribute Attribute { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="socket-type", Namespace="automation.control")]
	public class SocketType {
		[XmlElement(ElementName="socket-parameter", Namespace="automation.control")]
		public List<SocketParameter> SocketParameter { get; set; } = null!;

		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="attribute-type", Namespace="system")]
	public class AttributeType {
		[XmlAttribute(AttributeName="name")]
		public string Name { get; set; } = null!;

		[XmlAttribute(AttributeName="type")]
		public string Type { get; set; } = null!;

		[XmlAttribute(AttributeName="title")]
		public string Title { get; set; } = null!;

		[XmlAttribute(AttributeName="description")]
		public string Description { get; set; } = null!;

		[XmlAttribute(AttributeName="allow-multiple")]
		public string AllowMultiple { get; set; } = null!;

		[XmlAttribute(AttributeName="can-be-inherited")]
		public string CanBeInherited { get; set; } = null!;

		[XmlAttribute(AttributeName="uuid")]
		public string Uuid { get; set; } = null!;
	}

	[XmlRoot(ElementName="omx", Namespace="system")]
	public class Omx {
		[XmlElement(ElementName="link-unit", Namespace="system")]
		public List<LinkUnit> LinkUnit { get; set; } = null!;

		[XmlElement(ElementName="domain", Namespace="automation.deployment")]
		public Domain Domain { get; set; } = null!;

		[XmlElement(ElementName="namespace", Namespace="system")]
		public List<Namespace> Namespace { get; set; } = null!;

		[XmlAttribute(AttributeName="xmlns")]
		public string Xmlns { get; set; } = null!;

		[XmlAttribute(AttributeName="migration")]
		public string Migration { get; set; } = null!;

		[XmlAttribute(AttributeName="dp", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Dp { get; set; } = null!;

		[XmlAttribute(AttributeName="eth", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Eth { get; set; } = null!;

		[XmlAttribute(AttributeName="srv", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Srv { get; set; } = null!;

		[XmlAttribute(AttributeName="ct", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Ct { get; set; } = null!;

		[XmlAttribute(AttributeName="a", Namespace="http://www.w3.org/2000/xmlns/")]
		public string A { get; set; } = null!;

		[XmlAttribute(AttributeName="r", Namespace="http://www.w3.org/2000/xmlns/")]
		public string R { get; set; } = null!;

		[XmlAttribute(AttributeName="opc", Namespace="http://www.w3.org/2000/xmlns/")]
		public string Opc { get; set; } = null!;
	}