# CC.Yi

#### 介绍
基于.NET的MVC三成架构的框架--Yi意框架（意义是为了开发更简易）

第一版开发完成时间：2021/3/19 （请记住，这是一个值得纪念的日子）


#### 软件架构
架构：.NET5 +mysql+sql server（后期我们将集成数据+前端Vue，让开发更加简易！）

操作系统：Windows，Linux

扩展：EFcore，Autofac，Castle，Redis，Swagger，T4 ，Nlog（后期我们会添加更多）

封装：Json处理模块，滑动验证码模块，base64图片处理模块（后期我们会添加更多）

思想：简单工厂模式，抽象工厂模式，观察者模式，面向AOP思想，面向对象开发


#### 目录结构
![输入图片说明](https://images.gitee.com/uploads/images/2021/0321/023715_59bef411_3049273.png "屏幕截图.png")

Model：模型层（first code代码优先添加模型，数据自动生成）

DAL：数据处理层（处理数据但并未加载进内存）

BLL：业务逻辑层（数据的逻辑将在这层处理）

Common：工具层（工具人层，方法已封装，一键调用）

API：接口层（接入Swagger，可视化测试接口）


#### 安装教程
我们将在之后更新教程手册！

1.  下载全部源码
2.  使用Visual Studio 2019在windows环境中打开CC.Yi.sln文件即可


#### 使用说明
我们将在之后更新教程手册！

1.  添加一个数据库，并修改连接数据库的配置文件
2.  添加模型类，使用Add-Migration xxx迁移，再使用Update-Database更新数据库
3.  向T4Model添加模型名，一键转换生成T4
4.  控制器构造函数进行依赖注入直接使用

#### 联系我们：
QQ：454313500


