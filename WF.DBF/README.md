# DBF/Excel 转换工具

这是一个用于DBF和Excel文件转换的Windows桌面应用程序，使用C#和Windows Forms开发。该工具提供了简单直观的界面，可以轻松地读取、转换和导出DBF和Excel文件。

## 功能特点

- **DBF文件读取**：读取DBF文件内容，显示表头和数据
- **Excel文件读取**：读取Excel文件内容，显示表头和数据
- **格式转换**：
  - DBF转Excel
  - Excel转DBF
- **自定义导出**：
  - 可选择需要导出的字段
  - 可设置字段默认值
  - 可自定义字段名称和长度
- **大文件处理**：
  - 异步处理大型文件，避免界面卡顿
  - 进度条显示处理进度
  - 支持取消长时间运行的操作
- **帮助文档**：内置Markdown格式的帮助文档

## 系统要求

- Windows 7/8/10/11 (64位)
- 无需安装.NET运行时（已包含在应用程序中）
- 至少500MB可用磁盘空间

## 使用方法

1. 下载并解压缩应用程序
2. 双击`WF.DBF.exe`运行程序
3. 使用界面上的按钮选择要读取的DBF或Excel文件
4. 根据需要进行转换或导出操作

## 开发环境

- Visual Studio 2022或JetBrains Rider
- .NET 8.0
- Windows Forms

## 项目结构

```
WPF.DBF/
├── WF.DBF/                  # 主项目目录
│   ├── Form1.cs             # 主窗体
│   ├── ProgressForm.cs      # 进度条窗体
│   ├── Utils/               # 工具类
│   │   ├── DBFUtil.cs       # DBF文件处理工具
│   │   └── ExcelUtil.cs     # Excel文件处理工具
│   └── Resources/           # 资源文件
│       └── md/              # Markdown文档
│           └── Introduce.md # 帮助文档
└── Properties/              # 项目属性
    └── PublishProfiles/     # 发布配置文件
```

## 构建项目

### 使用Visual Studio

1. 打开`WPF.DBF.sln`解决方案文件
2. 选择`Release`配置
3. 右键点击项目，选择`发布`
4. 选择`SingleFileWin64`配置文件
5. 点击`发布`按钮

### 使用命令行

```bash
# 还原依赖项
dotnet restore

# 构建项目
dotnet build -c Release

# 发布项目
dotnet publish -p:PublishProfile=SingleFileWin64 -c Release
```

发布后的文件位于：`WF.DBF\bin\Release\net8.0-windows\publish\win-x64\`目录下。

## 打包方法

### 单文件应用程序

项目已配置为生成单文件应用程序，包含所有必要的依赖项。打包步骤如下：

1. 修改`WF.DBF.csproj`文件，确保包含以下配置：

```xml
<PropertyGroup>
  <PublishSingleFile>true</PublishSingleFile>
  <SelfContained>true</SelfContained>
  <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  <PublishReadyToRun>true</PublishReadyToRun>
  <IncludeNativeLibrariesForSelfExtract>true</IncludeNativeLibrariesForSelfExtract>
</PropertyGroup>
```

2. 创建发布配置文件`Properties/PublishProfiles/SingleFileWin64.pubxml`：

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project>
  <PropertyGroup>
    <Configuration>Release</Configuration>
    <Platform>Any CPU</Platform>
    <PublishDir>bin\Release\net8.0-windows\publish\win-x64\</PublishDir>
    <PublishProtocol>FileSystem</PublishProtocol>
    <TargetFramework>net8.0-windows</TargetFramework>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <SelfContained>true</SelfContained>
    <PublishSingleFile>true</PublishSingleFile>
    <PublishReadyToRun>true</PublishReadyToRun>
    <PublishTrimmed>false</PublishTrimmed>
    <IncludeNativeLibrariesForSelfExtract>true</IncludeNativeLibrariesForSelfExtract>
  </PropertyGroup>
</Project>
```

3. 执行发布命令：

打包成单体可执行文件(体积: 205MB): 
```bash
dotnet publish -p:PublishProfile=SingleFileWin64 -c Release
```

压缩打包程序(体积: 85MB):
```bash
 dotnet publish -p:PublishProfile=compressedsize -c release
```

优化启动后的版本:
```bash
dotnet publish -p:PublishProfile=FastStartup -c Release
```

4. 发布后的文件位于：`WF.DBF\bin\Release\net8.0-windows\publish\win-x64\`目录下

### 分发应用程序

1. 复制整个`win-x64`文件夹
2. 创建ZIP压缩包进行分发
3. 或者使用Inno Setup等工具创建安装程序

## 为其他平台打包

如需为其他平台创建可执行文件，修改项目文件中的`RuntimeIdentifier`参数：

- 32位Windows：`win-x86`
- ARM64 Windows：`win-arm64`
- macOS：`osx-x64`或`osx-arm64`（适用于Apple Silicon）
- Linux：`linux-x64`

然后重新运行发布命令。

## 依赖项

- ClosedXML (0.104.2)
- Markdig (0.40.0)
- Newtonsoft.Json (13.0.3)
- SocialExplorer.FastDBF (1.0.0)
- System.Collections.NonGeneric (4.3.0)
- System.Data.OleDb (9.0.2)
- System.Text.Encoding.CodePages (9.0.2)

## 许可证

[MIT License](LICENSE)

## 贡献

欢迎提交问题和改进建议！

## 联系方式

如有问题或建议，请联系[您的联系方式]。
