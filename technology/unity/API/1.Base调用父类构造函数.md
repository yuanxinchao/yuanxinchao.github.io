## 使用Base先调用父类构造函数
下面的代码即先调用父类BaseContext的构造函数，且参数为`UIType.MainMenu`

    public class MainMenuContext : BaseContext
    {
        public MainMenuContext() : base(UIType.MainMenu)
        {

        }
    }