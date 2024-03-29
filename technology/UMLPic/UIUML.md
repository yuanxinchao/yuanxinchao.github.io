## UML图
[note:每一个具体View的预设里都挂有一个脚本，脚本里有两个Class\n一个是对应的View一个是对应的Context，\n这样在unity中对对应的UIGameObject使用GetComponent<BaseView>()就可以以持有对应的ClassView就可以调用其OnEnter，OnExit等方法，\n这里找到对应的UIGameObject的方法使用的是用UIType对应到字典里{bg:cornsilk}]
[MonoBehaviour{bg:skyblue}]^[BaseView||+OnEnter(BaseContext context):void;+OnExit(BaseContext context):void;+OnPause(BaseContext context):void;+OnResume(BaseContext context):void;+DestroySelf():void{bg:skyblue}]
[BaseView{bg:skyblue}]++-1>[BaseContext|+ViewType:UIType|+BaseContext(UIType viewType){bg:skyblue}]
[BaseView{bg:skyblue}]^[AnimateView|animator:Animator|+OnEnter(BaseContext context):void;+OnExit(BaseContext context):void;+OnPause(BaseContext context):void;+OnResume(BaseContext context):void;{bg:skyblue}]
[UIType|+Path:string;+Name:string;+_MainMenu:UIType;+_OptionMenu:UIType;+_NextMenu:UIType;+_HighScore:UIType;|+UIType(string path);+ToString():string{bg:skyblue}]
[UIType{bg:skyblue}]->[UIType{bg:skyblue}]
[BaseContext{bg:skyblue}]++-1>[UIType{bg:skyblue}]
[BaseContext{bg:skyblue}]^[MainMenuContext||+MainMenuContext(UIType.MainMenu){bg:skyblue}]
[BaseContext{bg:skyblue}]^[NextMenuContext||+NextMenuContext(UIType.NextMenu){bg:skyblue}]
[BaseContext{bg:skyblue}]^[OptionMenuContext||+OptionMenuContext(UIType.OptionMenu){bg:skyblue}]
[BaseContext{bg:skyblue}]^[HighScoreContext||+HighScoreContext(UIType.HighScore){bg:skyblue}]
[AnimateView{bg:skyblue}]^[MainMenuView||+OnEnter(BaseContext context):void;+OnExit(BaseContext context):void;+OnPause(BaseContext context):void;+OnResume(BaseContext context):void;+OptionCallBack():void;+HighScoreCallBack():void{bg:skyblue}]
[AnimateView{bg:skyblue}]^[NextMenuView||+OnEnter(BaseContext context):void;+OnExit(BaseContext context):void;+OnPause(BaseContext context):void;+OnResume(BaseContext context):void;+BackCallBack ():void;+ChangeLangCallBack ():void{bg:skyblue}]
[AnimateView{bg:skyblue}]^[HighScoreView||+OnEnter(BaseContext context):void;+OnExit(BaseContext context):void;+OnPause(BaseContext context):void;+OnResume(BaseContext context):void;+BackCallBack():void{bg:skyblue}]
[AnimateView{bg:skyblue}]^[OptionMenuView||+OnEnter(BaseContext context):void;+OnExit(BaseContext context):void;+OnPause(BaseContext context):void;+OnResume(BaseContext context):void;+BackCallBack():void;+NextCallBack():void{bg:skyblue}]  

[OptionMenuViewOBJ]->[OptionMenuView]
[OptionMenuViewOBJ]->[OptionMenuContext]
[MainMenuViewOBJ]->[MainMenuView]
[MainMenuViewOBJ]->[MainMenuContext]
[NextMenuViewOBJ]->[NextMenuView]
[NextMenuViewOBJ]->[NextMenuContext]
[HighScoreViewOBJ]->[HighScoreView]
[HighScoreViewOBJ]->[HighScoreContext]

[UIManager|+UIDict:Dictionary<UIType, GameObject>]
[UIManager]->[MainMenuViewOBJ]
[UIManager]->[OptionMenuViewOBJ]
[UIManager]->[NextMenuViewOBJ]
[UIManager]->[HighScoreViewOBJ]
[UIManager]->[UIType]

[ContextManager|+contextStack:BaseContext|+UIManager:UIManager]->[UIManager]
[ContextManager]->[BaseContext]

![UML1](./UML/UML1.png)