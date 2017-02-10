# NO.1 Unity切图
### 利用脚本生成切图插件
> * 脚本路径在Editor下的SpriteSheetPacker，属于UnityEditor类的，意味着不是在运行时工作，而是在编辑时工作。
> * Unity引擎会自动检测到Editor文件下的UnityEditor类，可以看到导航栏的Assets下多了个Sprite Sheet Packer。

### 前期准备
> * 把图片Texture Type改为"Sprite"，Sprite Mode改为"Multiple"。
> * 打开Sprite Editor，左上角选择Slice，可以自动切图，或者手动调整，然后应用，会自动生成一张张图片，但此时图片的信息只是存在meta中，需要导出图片。

### 开始切图
> * 把图片Texture Type改为"Advanced"，将"Read/Write Enabled"属性进行打勾。
> * 选中切图后的图片，然后在菜单栏依次选择Assets->Sprite Sheet Packer->Process to Sprites，此时生成的图片放在同一路径上的同名文件夹。

# NO.2 Unity透视Shader
### 尽情期待
