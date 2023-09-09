using System;

public class EventCode
{
	public const byte GameList = 230;

	public const byte GameListUpdate = 229;

	public const byte QueueState = 228;

	public const byte Match = 227;

	public const byte AppStats = 226;

	public const byte AzureNodeInfo = 210;

	public const byte Join = 255;

	public const byte Leave = 254;

	public const byte PropertiesChanged = 253;

	[Obsolete("Use PropertiesChanged now.")]
	public const byte SetProperties = 253;
}