# Unity-Tricks游戏开发小姿势

## 客户端
### NO.1 在圆内随机生成点
我们知道圆内的笛卡尔方程为：
$$ x^2 + y^2 <= r^2 $$
r是圆的半径，满足这个方程的x, y即在圆内。
我们想在这个圆内等概率生成点，就相当于有无数个圆环，即：
$$ x^2 + y^2 = random * (r)^2 $$
转化成极坐标为
$$ ρ = \sqrt{random} * r $$
极坐标的的角度也是随机的
$$ θ = 2 * \pi * random $$
$$ x = x\_center + ρ * cos(θ) $$
$$ y = y\_center + ρ * sin(θ) $$

### NO.2 范围检测
> * 玩家范围检测就是判断怪物是否在玩家一定`距离`和`角度`（呈现扇形区域）内，因此需要分别计算距离和角度。
> * 玩家和怪物的`距离`可以通过`Vector3.Distance`计算。
> * 玩家和怪物的`角度`可以通过`Mathf.Acos`计算，然后乘以`Mathf.Rad2Deg`转化为度数。

### No.3 射线拾取
> * Physics.Raycast(ray, out hit)

### NO.4 画图
> * LineRender组件，制作预制体。

### NO.5 跑马灯
1. 原理
> * 跑马灯有区域限制，超出这个区域就不显示，这里我们用`Mask遮罩`实现。
> * 以水平跑马灯为例：跑马灯的可视范围是背景宽度，文字从右边开始到左边结束，总共移动的距离是`背景宽度 + 文字宽度`。
> * 跑马灯的动画实现使用了`DOTween插件`。[插件](https://assetstore.unity.com/)

2. 前期准备
> * 新建一个Image作为背景。调整适当大小。
> * 背景下再新建一个Image。添加Mask组件，用于遮住背景之外的文字，Rect Transfrom设置为Stretch，四维全部设置为0，铺满背景。
如果是水平滚动的将Rect Transform的Pivot设置为`1 0.5`，令Mask锚点位于`右边`。
如果是垂直滚动的将Rect Transform的Pivot设置为`0.5 0`，令Mask锚点位于`下边`。
> * Mask下创建Text，随意写些文字，居中显示，添加Content Size Fitter。
如果是水平滚动的将`Horizontal Fit`设置为Preferred Size，将Rect Transform的Pivot设置为`0 0.5`，令Text锚点位于Mask处，方便实现从右往左动画。
如果是垂直滚动的将`Vertical Fit`设置为Preferred Size，将Rect Transform的Pivot设置为`0.5 1`，令Text锚点位于Mask处，方便实现从下往上动画。

### NO.6 摇杆
> * `定义委托`  
public delegate void JoyStickTouchBegin(Vector2 vec);  // 定义触摸开始事件委托  
public delegate void JoyStickTouchMove(Vector2 vec);  // 定义触摸过程事件委托  
public delegate void JoyStickTouchEnd();  // 定义触摸结束事件委托  
> * `注册事件`  
public event JoyStickTouchBegin OnJoyStickTouchBegin;  // 注册触摸开始事件  
public event JoyStickTouchMove OnJoyStickTouchMove;  // 注册触摸过程事件  
public event JoyStickTouchEnd OnJoyStickTouchEnd;  // 注册触摸结束事件  
> * `使用接口`  
IPointerDownHandler, IPointerUpHandler, IDragHandler  
public void OnPointerDown(PointerEventData eventData)  // 触摸开始  
public void OnPointerUp(PointerEventData eventData)  // 触摸结束  
public void OnDrag(PointerEventData eventData)  // 触摸过程  
> * `返回摇杆的偏移量`  
```
private Vector2 GetJoyStickAxis(PointerEventData eventData)
{
    // 获取手指位置的世界坐标
    Vector3 worldPosition;
    if (RectTransformUtility.ScreenPointToWorldPointInRectangle(selfTransform,
             eventData.position, eventData.pressEventCamera, out worldPosition))
        selfTransform.position = worldPosition;
    // 获取摇杆偏移量
    Vector2 touchAxis = selfTransform.anchoredPosition - originPosition;
    // 摇杆偏移量限制
    if (touchAxis.magnitude >= JoyStickRadius)
    {
        touchAxis = touchAxis.normalized * JoyStickRadius;
        selfTransform.anchoredPosition = touchAxis;
    }
    return touchAxis;
}
```

### NO.7 小地图
> * `UI准备`：Mask圆形遮罩，Minimap小地图边框。
> * 添加一个新的相机，并命名为`Mini Camera`。然后将该相机设为 Player 的子对象，position设为(0, 10,0)，rotation设为(90, 0, 0)。
> * 渲染到UI层需要用到Render Texture来实现。依次点击菜单项Assets -> Create -> Render Texture新建Render Texture，并命名为`Minimap Render`。选中Mini Camera后将Target Texture设为Minimap Render。
> * 下面新建Canvas来添加UI元素。新建Raw Image，命名为`Map`，将Texture设为Minimap Render。
> * 下面新建Image，命名为`Mask`，为其添加Mask组件，并将Image的Source Image设为上面的圆形遮罩。最后将Map设为Mask的子对象。
> * 下面新建Image，命名为`Outline`，将Image的Source Image设为上面的小地图边框。
> * 为了让整个小地图移动起来更方便，新建一个空的GameObject命名为`Minimap`，并将所有对象设为Minimap子对象。
> * 最后层级如下：  
`Minimap`  
---- `Mask`  
-------- `Map`  
---- `Outline`  

### NO.8 插值移动
> * 创建2D精灵Sprite，命名为`Player`，作为我们的主角。
> * 切图。Sprite - Multiple - Sprite Editor（可能需要安装插件Package Manager - 2D Sprite） - Splice - Apply。
> * 制作人物休息和运动动画。按住CTRL选中几张帧动画图片拖到Inspector上的主角，可以快速生成动画，命名为`Idle`和`Run`，同时生成主角同名动画状态机Player。
> * 双击Player动画状态机可以直接打开Animator视图，将Idle和Run拖到视图，分别右键`Make Transition`。
> * 在Animator视图的左侧可以选择Parameters，创建Bool型参数`Run`，作为我们的转换条件。
> * 通过上面的步骤，我们设置PlayerIdle到PlayerRun的转换条件为Run `True`，PlayerRun到PlayerIdle的转化条件为Run `False`。
> * `Has Exit Time` = False
> * `Transtion Duration` = 0
> * 否则动画切换的时候会不及时，因为转换到下一个动画之前必须等待当前动画播放完毕。

> * 线性插值 `Vector3.Lerp(Vector3 from, Vector3 to, float smoothing)` 。
> * 公式 `t = from + (to - from) * smoothing`。
> * from为初始位置，to为结束位置，smoothing为平滑速度，返回t为线性插值计算出来的向量，范围在 [0...1]之间。

### NO.9 分页
> * 制作Grid  
1.新建Image，改名`Grid`作为头像。  
2.新建Image作为`Grid`子物体，改名为`Item`作为物品名字背景。  
3.新建Text作为`Item`子物体，改名为`Name`作为物品名字。  
4.将物体制作成Prefab，最后层次关系应该是：
Grid  
----Item  
--------Name  
> * 自动排版  
1.新建Panel，将Grid作为Panel子物体，再将Grid复制12份。  
2.在Panel下添加`Grid Layout Group`组件，调整Padding、Cell Size、Spacing到合适位置，可以看到子物体全部自动排版。

### NO.10 聊天框
> * 重点难点：  
1.需要控制别人和自己聊天框Item的位置  
2.需要控制聊天框ScrollView的滚动  
3.需要控制聊天框Item的宽度高度  
4.需要控制聊天框ScrollView的伸长  
5.需要移除历史聊天框Item  
> * 基本UI组件有玩家输入框、发送按钮、聊天框Item、聊天框ScrollView。
> * 聊天框Item有left和right两种，分别是别人和自己，以自己的聊天框right为例子:  
1.新建一个Image作为`背景`，设置Anchor为(right, top)、Pivot为(1, 1)。  
2.在背景下新建一个Image作为`头像`，设置Anchor为(right, bottom)和一个Text作为`文字`。  
3.在头像下新建一个Text作为`名字`，设置Anchor为(right, middle)。  
4.挂上ChatUI脚本，专门控制UI显示。
5.将其制作成为Prefab，聊天框left同理。  
> * 聊天框ScrollView：  
新建一个ScrollView，设置Anchor为(stretch, stretch)，调整为适当大小。  

## [DOTS](https://unity.com/cn/dots/packages)
DOTS（Data-Oriented Tech Stack，面向数据的技术堆栈）：ECS 、Job System、Burst Compiler。
ECS：数据和逻辑解耦，CPU缓存友好。
Job System：多核编程。
Burst Compiler：优化编译。

> [UnityECS学习日记](https://blog.csdn.net/qq_36382054/category_9596750.html)
> [EntityComponentSystemSamples](https://github.com/Unity-Technologies/EntityComponentSystemSamples)

导包：Window -> Package Manager -> Add package from git URL -> com.unity.rendering.hybrid
调试：Window -> Analysis -> Entity Debugger

## 图形学
### 光照
1. Lambert（兰伯特）：漫反射
$$ I_{diff} = K_d \ast I_l \ast (N \cdot L) $$

2. Half-Lambert（半-兰伯特）：漫反射优化
$$ I_{diff} = K_d \ast I_l \ast (\alpha (N \cdot L) + \beta) $$

3. Phong（冯氏）：高光反射
$$ I_{spec} = K_s \ast I_l \ast (V \cdot R)^{n_s} $$

$$ R = 2 \ast (N \cdot L) \ast N - L $$

4. Blinn-Phong（布林-冯氏）：高光反射优化
$$ I_{spec} = K_s \ast I_l \ast (N \cdot H)^{n_s} $$

$$ H = \frac{L + V}{\vert L + V \vert} $$

> [基础](https://zhuanlan.zhihu.com/p/89626438)
> [漫反射](https://zhuanlan.zhihu.com/p/89628885)
> [高光反射](https://zhuanlan.zhihu.com/p/89630608)

### 遮挡透视
第一遍透视绘制：物体遮挡，ZWrite Off、Greater。（关闭深度缓存）
第二遍正常绘制：正常渲染。

### 边缘发光
第一遍描边绘制：法线外扩。ZWrite Off、Always。（关闭深度缓存）
第二遍正常绘制：正常渲染。

## 算法
### A* 寻路算法
估价函数：f(n) = g(n) + h(n)
g(n)：从起点到节点n的最短路径。
h(n)：从节点n到终点的最短路径的启发值。
曼哈顿距离：h(n) = x + y
特殊情况：当h(n)等于0时，A*算法等于Dijkstra算法。
```
while(OPEN!=NULL)
{
    从OPEN表中取f(n)最小的节点n;
    if(n节点==目标节点)
        break;
    for(当前节点n的每个子节点X)
    {
        计算f(X);
        if(XinOPEN)
            if(新的f(X)<OPEN中的f(X))
            {
                把n设置为X的父亲;
                更新OPEN表中的f(n);
            }
        if(XinCLOSE)
            continue;
        if(Xnotinboth)
        {
            把n设置为X的父亲;
            求f(X);
            并将X插入OPEN表中;//还没有排序
        }
    }//endfor
    将n节点插入CLOSE表中;
    按照f(n)将OPEN表中的节点排序;//实际上是比较OPEN表内节点f的大小，从最小路径的节点向下进行。
}//endwhile(OPEN!=NULL)
```

### Scene 异步加载
略
### FSM 状态机
略
### Pool 对象池
略
### Audio 音效管理
略

