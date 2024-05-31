using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

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

        #region 3.环境变量提供应用程序

        ////dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
        //var builder = new ConfigurationBuilder();
        //builder.AddEnvironmentVariables();


        //var configurationRoot = builder.Build();
        ////属性--> 调试里面 设置的环境变量的 参数
        //Console.WriteLine($"key1:{configurationRoot["key1"]}");

        //#region 分层键
        //Console.WriteLine($"key3:{configurationRoot.GetSection("select")["key3"]}");
        //Console.WriteLine($"key4:{configurationRoot.GetSection("select:key3")["key4"]}");
        //#endregion

        //#region 前缀过滤
        //builder.AddEnvironmentVariables("cy_");
        //configurationRoot = builder.Build();
        //Console.WriteLine($"cy key1:{configurationRoot["key1"]}");
        //#endregion

        #endregion

        #region 4.文件配置提供程序
        //dotnet add package Microsoft.Extensions.Configuration.Ini
        //dotnet add package Microsoft.Extensions.Configuration.Json
        //dotnet add package Microsoft.Extensions.Configuration.NewtonsoftJosn
        //dotnet add package Microsoft.Extensions.Configuration.Xml
        //dotnet add package Microsoft.Extensions.Configuration.UserSecrets

        //var builder = new ConfigurationBuilder();
        ////optional:false 检测文件存不存在 默认false,当是true的时候,如果文件不存在则会报错~
        ////reloadOnChange 是否读取文件变更
        //builder.AddJsonFile("appsettings.json",optional:false,reloadOnChange:true);
        //builder.AddIniFile("appsettings.ini",optional:false,reloadOnChange:true);
        //var configurationRoot = builder.Build();

        //Console.WriteLine($"key1:{configurationRoot["key1"]}");
        //Console.WriteLine($"key2:{configurationRoot["key2"]}");
        //Console.WriteLine($"key3:{configurationRoot["key3"]}");
        //Console.ReadKey();


        //Console.WriteLine($"key1:{configurationRoot["key1"]}");
        //Console.WriteLine($"key2:{configurationRoot["key2"]}");
        //Console.WriteLine($"key3:{configurationRoot["key3"]}");



        //var connectionString = configurationRoot["DatabaseSettings:ConnectionString"];
        //var logLevel = configurationRoot["Logging:LogLevel"];
        //var apiKey = configurationRoot["ApiSettings:ApiKey"];

        //Console.WriteLine($"Connection String: {connectionString}");
        //Console.WriteLine($"Log Level: {logLevel}");
        //Console.WriteLine($"API Key: {apiKey}");

        //Console.ReadKey();
        #endregion

        #region 5.配置变更监听

        //var builder = new ConfigurationBuilder();
        //builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //var configurationRoot = builder.Build();

        //IChangeToken changeToken = configurationRoot.GetReloadToken();

        ////注意:Debug 模式下在 \bin\Debug\net8.0\的生成文件下去修改文件内容 才生效!!!

        //////只能获取一次变更!
        ////changeToken.RegisterChangeCallback(state =>
        ////{
        ////    Console.WriteLine($"key1---->:{configurationRoot["key1"]}");
        ////    Console.WriteLine($"key2---->:{configurationRoot["key2"]}");
        ////    Console.WriteLine($"key3---->:{configurationRoot["key3"]}");
        ////}, configurationRoot);


        ////多次变更都能捕获到!
        //ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
        //{
        //    Console.WriteLine($"key1---->:{configurationRoot["key1"]}");
        //    Console.WriteLine($"key2---->:{configurationRoot["key2"]}");
        //    Console.WriteLine($"key3---->:{configurationRoot["key3"]}");
        //});

        //Console.ReadKey();

        #endregion

        #region 6.配置绑定
        //dotnet add package Microsoft.Extensions.Configuration.Binder

        //1.绑定已有对象
        //2.绑定私有属性

        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var config = new AppSettingConfigModel() 
        {
            Key1 = "Key1",
            Key2 = "Key2",
            //Key4 = 4
        };
        var configurationRoot = builder.Build();
        configurationRoot.Bind(config); //以appsettings.json 的为准

        //多级设置
        configurationRoot.GetSection("OrderService").Bind(config, options => 
        {
            //private 的私有属性,也可以赋值进去
            options.BindNonPublicProperties = true;
        });

        Console.WriteLine($"Key2---->:{config.Key1}");
        Console.WriteLine($"Key2---->:{config.Key2}");
        Console.WriteLine($"Key4---->:{config.Key4}");

        #endregion

        #region 7.结合IOption<> 使用 选项框架

        #endregion

        #region 8.为选项框架添加验证逻辑

        #endregion
    }

    class AppSettingConfigModel
    {
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public string Key3 { get; set; }
        public int Key4 { get; private set; } = 400;
        public bool Key5 { get; set; }
    }
}