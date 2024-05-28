using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] ages)
    {
        #region 1. 自定义Dictionary Config 内存字典模式
        //dotnet add package Microsoft.Extensions.Configuration

        //IConfigurationBuilder builder = new ConfigurationBuilder();
        //builder.AddInMemoryCollection(new Dictionary<string, string>()
        //{
        //    {"key1","v1"},
        //    {"key2","v2"},
        //    {"key3","v3"},
        //    {"key4:key5","v4-v5"},
        //}!);
        //IConfigurationRoot configurationRoot = builder.Build();
        //IConfiguration config = configurationRoot;

        //Console.WriteLine(configurationRoot["key1"]);
        //Console.WriteLine(configurationRoot["key2"]);

        //IConfigurationSection section = config.GetSection("key4");

        //Console.WriteLine(section["key5"]);
        //Console.WriteLine(config["key4:key5"]);
        //Console.WriteLine(configurationRoot["key4:key5"]); 
        #endregion

        #region 2 命令替换模式/提供程序
        ////dotnet add package Microsoft.Extensions.Configuration.CommandLine
        //var bulider = new ConfigurationBuilder();

        ////bulider.AddCommandLine(ages);

        //#region 命令替换模式/提供程序

        //var mapper = new Dictionary<string, string>()
        //{
        //    {"-k1","CommandLineKey1" }
        //};
        //bulider.AddCommandLine(ages,mapper);
        ////结果就会变为 LineKey:k3
        ////通常用于 短命名,快捷命名

        //#endregion

        //var configurationRoot = bulider.Build();
        ////属性--> 调试里面设置的启动参数
        //Console.WriteLine($"LineKey:{configurationRoot["CommandLineKey1"]}");
        //Console.WriteLine($"LineKey:{configurationRoot["CommandLineKey2"]}");

        //Console.ReadKey(); 
        #endregion

        //3.环境变量提供应用程序
        //dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
        var builder = new ConfigurationBuilder();
        builder.AddEnvironmentVariables();

        var configurationRoot = builder.Build();
        //属性--> 调试里面 设置的环境变量的 参数
        Console.WriteLine($"key1:{configurationRoot["key1"]}");

        #region 分层键
        Console.WriteLine($"key3:{configurationRoot.GetSection("select")["key3"]}");
        Console.WriteLine($"key4:{configurationRoot.GetSection("select:key3")["key4"]}");
        #endregion

        #region 前缀过滤
        builder.AddEnvironmentVariables("cy_");
        configurationRoot = builder.Build();
        Console.WriteLine($"cy key1:{configurationRoot["key1"]}");
        #endregion

    }
}