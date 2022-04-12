# Tuya.Net ![build](https://github.com/thatbrainiac/Tuya.Net/actions/workflows/dotnet.yml/badge.svg)
A Tuya API client library written for .NET 6. Extend your smart home functionality.

## Prerequisites

To use this library you must have an active account on the [Tuya developer platform](https://iot.tuya.com/) and be subscribed to the `IoT Core` and `Authorizaiton` services. Once you have access to the developer platform, you can obtain an authorization key from there.

This library is currently aimed to end users who want to extend automation functionality, so you must also have an end user account.

Your end user account must be linked/authorized on your Tuya developer account as well, otherwise you will not have access to your resources. To link your Tuya user account: Go to your project -> Devices -> Link Tuya App Account.

For more information, visit the [Tuya official documentation](https://developer.tuya.com/en/docs/iot/link-devices?id=Ka471nu1sfmkl#title-6-Link%20Tuya%20app%20account).

## Installation

NuGet Package: [Tuya.Net]()

## Examples

Below are some examples and use cases where this library may be found useful.

### Create an instance of the Tuya API Client
```csharp
const string TuyaApiUrl = "https://openapi.tuyaeu.com";
var tuyaCreds = new TuyaCredentials()
{
    ClientId = "your_client_id_here",
    ClientSecret = "your_client_secret_here"
};

client = await new TuyaClient(TuyaApiUrl, tuyaCreds)
    .WithAuthentication()
```

### Get device list for a given user ID

```csharp
var devices = await client.DeviceManager.GetDevicesByUserAsync("user_id_here"));
/// do something..
```

### Toggle a lighting device depending on its status

```csharp

// Retrieve the status of the lighting device to check whether the light is turned on or off.

var device = await client.DeviceManager.GetDeviceAsync("lighting_device_id_here");
var status = device?.StatusList?.FirstOrDefault(ds => ds.Code == "switch_led");

if (status?.Value is not bool)
{
    throw new Exception("Cannot obtain the value of the device status, the switch_led status did not return bool as expected.");
}

// Get the device status (true if the light is turned on, false otherwise)

var isTurnedOn = (bool)status.Value!;

// Create the command to send an instruction to maniuplate the light status

var command = new Command()
{
    Code = "switch_led",
    Value = !isTurnedOn, // switch the value
};

// Send the command and obtain the result from the server.

var result = await client.DeviceManager.SendCommandAsync(device, command); // returns true if the command was executed successfully, false otherwise.
```

## Contribute

Feel free to contribute to this project by creating pull requests.

### Building

To build this project, you require the following:

#### Using Visual Studio or Rider
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Rider 2021.3+](https://www.jetbrains.com/rider/)
- [.NET Core 6.0 SDK](https://dotnet.microsoft.com/en-us/download)

#### Using the Command Line

- [.NET Core 6.0 SDK](https://dotnet.microsoft.com/en-us/download)

### Testing

Integration tests are available, but some configurations must be passed first before running them. Make sure you add a valid client id and secret to your `secrets.json`:

Using the CLI, from the repository root:
```
dotnet user-secrets set "TuyaClientId" "YOUR_TUYA_CLIENT_ID_HERE" --project Tuya.Net.Tests
dotnet user-secrets set "TuyaClientSecret" "YOUR_TUYA_CLIENT_Secret_HERE" --project Tuya.Net.Tests
```

### Naming Conventions

Standard C# naming conventions apply, with the following exception:
- Private fields are _NOT_ prefixed with an underscore.
