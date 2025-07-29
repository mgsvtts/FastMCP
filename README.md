# Introduction

Test task for FastMCP and my first MCP experience! Any feedback is encouraged

# Design Choices
In a test task, there's always a trade-off between "showcasing technologies" and "building it like in the real world." I decided to go with the second option and kept the codebase simple, avoiding unnecessary dependencies such as MediatR, mappers, or HybridCache/FusionCache etc.
## Prerequisites

- .NET SDK 9
- Claude Desktop or VS Code with GitHub Copilot
- Git

## Installation

### Step 1: Clone the Repository

```bash
git clone https://github.com/mgsvtts/FastMCP.git
cd FastMCP
```

### (Optional) Step 1.1: Tests

You can run the command below to run tests

```bash
dotnet test
```

### Step 2: Configuration

#### For Claude Desktop

1. Locate your `claude_desktop_config.json` file
2. Add the MCP server configuration:

```json
{
  "mcpServers": {
    "mcp-weather-server": {
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "PATH_TO_THE_DOWNLOADED_PROJECT/FastMCP.csproj",
        "--no-build"
      ]
    }
  }
}
```

**Note:** Replace `PATH_TO_THE_DOWNLOADED_PROJECT` with the actual path to your cloned repository.

#### For VS Code with GitHub Copilot

The repository includes a `mcp.json` file, so no additional setup is required. Simply open the project in VS Code.

### Step 3: Usage

Once configured, you can ask your AI agent to show you current weather!

### (Optional) Step 4: Change API key

You can set your own api key for ``https://api.openweathermap.org`` in ``appsettings.json`` file

### (Optional) Step 5: Local server

You can use ``dotnet run`` command to run a local server, then you can send JSON RPC requests using tools like Postman