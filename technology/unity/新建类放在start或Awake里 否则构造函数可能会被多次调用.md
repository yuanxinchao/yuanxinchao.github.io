## 新建类放在start或Awake里 否则构造函数可能会被多次调用

unity中构造函数可能会被多次调用,官方建议使用start()/awake()初始化.
https://docs.unity3d.com/355/Documentation/ScriptReference/index.Writing_Scripts_in_Csharp_26_Boo.html


Using the constructor when the class inherits from MonoBehaviour, will make the constructor to be called at unwanted times and in many cases might cause Unity to crash.
Only use constructors if you are inheriting from ScriptableObject.

