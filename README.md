# NO.1 Unity切图
### 前期准备
> * 把图片`Texture Type`改为`Sprite`，`Sprite Mode`改为`Multiple`。
> * 打开`Sprite Editor`，左上角选择`Slice`，可以自动切图，或者手动调整，然后`Apply`，会自动生成一张张图片，但此时图片的信息只是存在meta中，需要导出图片。

### 切图效果编写与测试
> * 脚本路径在Editor下的SpriteSheetPacker，属于UnityEditor类的，意味着不是在运行时工作，而是在编辑时工作。
> * Unity引擎会自动检测到Editor文件下的UnityEditor类，可以看到导航栏的Assets下多了个Sprite Sheet Packer。
> * 把图片`Texture Type`改为`Advanced`，将`Read/Write Enabled`属性进行打勾。
> * 选中切图后的图片，然后在菜单栏依次选择`Assets`->`Sprite Sheet Packer`->`Process to Sprites`
，此时生成的图片放在同一路径上的同名文件夹。

# NO.2 Unity透视Shader
### 透视原理
> * 将玩家绘制多遍：`第一遍透视绘制`，当玩家被物体挡住，则绘制绿色像素。`第二遍正常绘制`：当玩家没有被物体挡住，则绘制正常颜色。
> * Z测试和Z比较：`物体到镜头的距离`就是Z轴（深度值），镜头处为0，镜头最大截面为1，越小越靠近镜头。
> * Z开启和Z关闭：`ON`保留镜头的Z值，屏幕上每个像素点都存储有当前最靠近镜头的物体的Z值，`OFF`避免屏幕存储错误的Z值，因为玩家被物体挡住。如果`打开Z测试`（深度缓存），我们就可以根据此深度值决定同样映射到此处的像素值是否可以写入到帧缓存中并绘制到屏幕上（最后产生的效果就是离相机越近的像素只会被写入到帧缓存，并绘制出来）。如果`关闭Z测试`（深度缓存），那么像素是否写入帧缓存就不依赖距离相机的远近（依赖于程序代码实现）。
> * Shader其他小知识：  
`pos`：屏幕上每个像素的位置。  
`UV`：屏幕上每个像素的纹理坐标，用于从贴图里面读取像素。  
`顶点着色器`（Vertex）：它执行在场景中的每个点，输出坐标投影，颜色，纹理和其他数据传递到片段着色器。  
`片段着色器`（Fragment）：它执行在每个像素，输出信息为像素的颜色。  

### 前期准备
> * 场景包含2个Plane，下面的Plane作为地板，上面的Plane取消了`Mesh Collider`，可以挡着玩家，方便测试下透视效果，另外还有4个Cube作为围墙。
> * 玩家由Capsule和Cube组成，Shader文件夹下有一个`Material`用来控制玩家的材质。
> * 控制玩家移动的脚本，放在Scripts下的`PlayerMove`。
> * 把脚本挂到玩家身上，记得还要给玩家添加一个`Rigidbody`组件，并勾上`Constraints`->`Freeze Rotation`->`X Z`，这是控制玩家只能朝着Y轴方向。

### 透视效果编写与测试
> * Shader文件夹下有一个`PlayerShader`，控制玩家透视效果。
> * Unity引擎会自动检测Shader文件，然后自动添加到Shader下拉列表。
> * 选择玩家`Shader`->`Custom`->`PlayerShader`。
> * 运行游戏，可以发现当玩家走到被物体挡住的地方时，会有黑色的阴影效果，成功。

# NO.3 文字打字效果
> * 场景中有`Text`，上面挂着`Typing`脚本。
> * 运行Main场景，可以看到文字呈现打字效果。

# NO.4 镜头震动效果
> * 场景中有`Camera`，上面挂着`Shaking`脚本。
> * 运行Main场景，可以看到镜头抖动效果。

# NO.5 玩家范围检测
### 基础知识
> * `Vector3.Distance(Vector3 a, Vector3 b)` 返回a和b之间的距离。
> * `Vector3.normalized` 向量标准化。（只读）
> * `Vector3.Dot(Vector3 lhs, Vector3 rhs)` 返回两个向量的点乘积。
> * `Mathf.Rad2Deg` 弧度到度的转化常量。（只读）
> * `Mathf.Acos(float f)` 以弧度为单位计算并返回参数 f 中指定的数字的反余弦值。
> * `Debug.DrawLine(Vector3 start, Vector3 end, Color color)` 从start起点到end末点，绘制一条color颜色的线。

### 基本思路
> * 玩家范围检测就是判断怪物是否在玩家一定`距离`和`角度`（呈现扇形区域）内，因此需要分别计算距离和角度。
> * 玩家和怪物的`距离`可以通过`Vector3.Distance`计算。
> * 玩家和怪物的`角度`可以通过`Mathf.Acos`计算，然后乘以`Mathf.Rad2Deg`转化为度数。

### 检测效果编写与测试
> * 场景有玩家和怪物，玩家来自NO.2。
> * 检测脚本名为`Detecting`，挂到玩家身上。
> * 运行游戏，可以看到如果范围内，则输出在，如果范围外，则输出不在。

# NO.6 镜头放大？旋转？
### 尽请期待

---
注：部分代码和文字来自网络，经过本人整合到本工程，有任何不明白都可以与我交流~~~
