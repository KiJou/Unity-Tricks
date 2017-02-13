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

### 场景设置
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
> * 场景有玩家和怪物，玩家设置同NO.2。
> * 范围检测脚本名为`Detecting`，挂到玩家身上。
> * 运行游戏，Game视图下点击Gizmos显示画图效果，可以看到如果范围内，则输出在，如果范围外，则输出不在。

# NO.6 镜头放大旋转
### 基础知识
> * `Vector.magnitude` 返回向量的长度，向量的长度是(x*x+y*y+z*z)的平方根。
> * `Mathf.Clamp(float value, float min, float max)` 限制value的值在min和max之间，如果value小于min，返回min。如果value大于max，返回max。否则返回value。
> * `Transform.RotateAround(Vector3 point, Vector3 axis, float angle)` 一个物体围绕 point位置 的 axis轴 旋转 angle角度

### 基本思路
> * transform.position = offsetPosition + player.position;  // 镜头跟随玩家
> * ScrollView();  // 控制镜头的拉近拉远  
`Input.GetAxis("Mouse ScrollWheel")` 鼠标向后滑动返回负数（拉近视野），向前滑动正数（拉远视野）
> * RotateView();  // 控制镜头的左右上下   
`Input.GetMouseButton(1)` 得到鼠标右键的按下  
`Input.GetAxis("Mouse X")` 得到鼠标水平方向的滑动  
`Input.GetAxis("Mouse Y")` 得到鼠标垂直方向的滑动  

### 镜头效果编写与测试
> * 场景有两个玩家（默认Player2可用），Player1设置同NO.2是用键盘WSAD控制玩家移动，Player2我们新增了一个脚本`PlayerMove2`来用鼠标左键控制玩家移动。
> * 控制镜头的脚本名为`FollowPlayer`，挂到镜头身上。
> * 运行游戏，鼠标`左键`可以控制玩家移动，鼠标`滑轮`可以控制镜头拉近拉远，鼠标`右键`可以控制镜头上下左右。

# NO.7 跑马灯
### 场景设置
> * 新建一个Image作为背景。调整适当大小。
> * 背景下再新建一个Image。添加Mask组件，用于遮住背景之外的文字，Rect Transfrom设置为Stretch，四维全部设置为0，铺满背景。  
如果是水平滚动的将Rect Transform的Pivot设置为`1 0.5`，令Mask锚点位于`右边`。  
如果是垂直滚动的将Rect Transform的Pivot设置为`0.5 0`，令Mask锚点位于`下边`。  
> * Mask下创建Text，随意写些文字，居中显示，添加Content Size Fitter。  
如果是水平滚动的将`Horizontal Fit`设置为Preferred Size，将Rect Transform的Pivot设置为`0 0.5`，令Text锚点位于Mask处，方便实现从右往左动画。  
如果是垂直滚动的将`Vertical Fit`设置为Preferred Size，将Rect Transform的Pivot设置为`0.5 1`，令Text锚点位于Mask处，方便实现从下往上动画。

### 跑马灯原理
> * 跑马灯有区域限制，超出这个区域就不显示，这里我们用`Mask遮罩`实现。
> * 以水平跑马灯为例：跑马灯的可视范围是背景宽度，文字从右边开始到左边结束，总共移动的距离是`背景宽度 + 文字宽度`。
> * 跑马灯的动画实现使用了`DOTween插件`。

### 跑马灯脚本解释
``` c#
float width = HText.preferredWidth;  // 获取文字的长度
HText.rectTransform.anchoredPosition = new Vector2(0, 0.5f);  // 让文字从在最右边开始移动  Tweener tweener = HText.rectTransform.DOLocalMoveX(-(bgWidth + width), duration);  // 设置动画持续时间
tweener.SetDelay(delay);  // 设置动画延迟时间
tweener.SetEase(Ease.Linear);  // 设置动画播放方式
tweener.SetLoops(5, LoopType.Restart);  // 每次播放结束后重新开始播放，一共播放 5 次
tweener.OnStart(delegate { Debug.Log("水平走马灯事件开始"); });  // 设置动画开始事件
tweener.OnComplete(delegate { Debug.Log("水平走马灯事件结束"); });  // 设置动画结束事件
```

# NO.8 贪吃蛇算法与基本实现
### 场景设置
> * `Main Camera` 2D游戏的基本设置：  
1.`Clear Flags`设置为Solid Color  
2.`Background`设置合适颜色，例如白色  
3.`Projection`设置为Orthographic  
4.`Size`设置合适大小，例如10  
> * 创建`Quad`作为背景，背景长度宽度都应该是`奇数`：  
1.上部分 + `中间1格` + 下部分 = 宽度  
2.左部分 + `中间1格` + 右部分 = 长度  
3.由此可知，在`上下左右对称`的情况下，1和2结果都是奇数  
> * 创建`Cube`，`Snake Food Wall`分别为蛇头 食物 墙壁的标签，将蛇头制作成`Prefab`，改名为SnakeBody作为蛇身。
> * 创建`Material`，`Red Blue Black`分别作为蛇 食物 墙壁的材质。注意Material Shader改成`Unlit/Color` 去掉阴影，成为2D图像。
> * 蛇 食物 墙壁的设置  
1.将`Box Collider`组件选择`Is Trigger`，碰撞效果为触发器。  
2.将`Size`改成`0.5`， 否则擦边而过也会判断为碰撞。  
3.都加上`Rigidbody`组件，取消`Gravity`，允许碰撞的同时防止因重力掉落。  

### 随机生成食物算法
``` c#
// 利用随机数可以简单实现
float x = Random.Range(-maxX, maxX);
float y = Random.Range(-maxY, maxY);
```

### 蛇身变长算法
``` c#
// 蛇头向前移动后，整条蛇可能是 [    （蛇尾）口口口口 ? 口（蛇头）    ] 即原来蛇头的位置空缺了。
// 我们可以把蛇尾 移动 到这个空缺的位置，即蛇尾不断地 补到 蛇头原来的位置。
snakeBody.Last().position = headPos;  // 蛇尾补到空缺的位置
snakeBody.Insert(0, snakeBody.Last());  // 蛇尾插入蛇身列表第一个位置
snakeBody.RemoveAt(snakeBody.Count - 1);  // 将原来的蛇尾从蛇身列表移除
```

### 游戏碰撞的各种情况
``` c#
void OnTriggerEnter(Collider other)  // Collider 碰撞检测
{
    if (other.gameObject.CompareTag(foodTag)  // 碰撞到食物
    {
        // 蛇身变长
        // 重新生成食物
    }
    else if (other.gameObject.CompareTag(snakeTag) ||
        other.gameObject.CompareTag(wallTag))  // 碰撞到自己或者墙壁
    {
        // 重新开始游戏
    }
}
```

---
注：部分代码和文字来自网络，经过本人整合到本工程，有任何不明白都可以与我交流~~~
