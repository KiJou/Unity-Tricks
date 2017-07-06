# Unity Tricks小把戏

## NO.1 精灵切图

## NO.2 资源加载

## NO.3 着色透视

## NO.4 录音功能

## NO.5 截屏功能

## NO.6 拍照功能

## NO.7 淡入淡出

## NO.8 文字打字

## NO.9 镜头震动

## NO.10 时间倒流

## NO.11 物体爆炸

## No.12 射线拾取

## NO.13 范围检测

## NO.14 昼夜交替

## NO.15 场景加载

## NO.16 虚拟摇杆

## NO.17 回合攻击

## NO.18 幸运转盘

## NO.19 背包分页

## NO.20 线条画图

## NO.21 弹力小球

## NO.22 单摆
1、创建一个Sphere球体作为重物，Scale修改为(0.2, 0.2, 0.2)，添加Rigidbody组件。  
2、创建一个Capsule胶囊体作为绳子，Position修改为(0, 1, 0)，Scale修改为(0.005, 1, 0.005)，添加Rigidbody组件，勾选Freeze PositionXYZ和Freeze RotationXY。  
3、最关键的一步到了，绳子添加Fixed Joint固定关节组件，并将Sphere拖到Connected Body上。固定关节基于另一个物体来限制一个物体的运动。  
4、还需要控制绳子的质心以及重物的初始受力，分别在Rope和Sphere两个脚本实现。

## NO.23 小地图
1、创建Camera，命名为[Mini Camera]，Position修改为(0, 10,0)，Rotation修改为(90, 0, 0)。  
2、创建Render Texture，命名为[Minimap Render]，选中[Mini Camera]后将Target Texture设置为[Minimap Render]。  
3、创建Raw Image，命名为[Map]，将Texture设置为[Minimap Render]。  
4、创建Image，命名为[Mask]，添加Mask组件，作为圆形遮罩。  
5、创建Image，命名为[Outline]，作为地图边框。  
6、创建GameObject，命名为[Minimap]，并将所有UI对象设置为[Minimap]子对象。  
7、最后UI层级如下：  
[Minimap]  
---- [Mask]  
-------- [Map]  
---- [Outline]

## NO.24 跑马灯
一、水平跑马灯：  
1、创建Image作为[Mask]，Rect Transfrom设置为Stretch，Pivot修改为(1, 0.5)，令Mask锚点位于右边。  
2、添加Mask组件，用于遮住背景之外的文字。  
3、Mask下创建Text作为[Content]，Pivot修改为(0, 0.5)。  
4、添加Content Size Fitter组件，将Horizontal Fit设置为Preferred Size，令Text锚点位于Mask处，方便实现从右往左动画。

二、垂直跑马灯：  
1、创建Image作为[Mask]，Rect Transfrom设置为Stretch，Pivot修改为(0.5, 0)，令Mask锚点位于下边。  
2、添加Mask组件，用于遮住背景之外的文字。  
3、Mask下创建Text作为[Content]，Pivot修改为(0.5, 1)。  
4、添加Content Size Fitter组件，将Vertical Fit设置为Preferred Size，令Text锚点位于Mask处，方便实现下往上动画。

## NO.25 聊天室
一、重点难点：  
1、需要控制别人和自己聊天框Item的位置。  
2、需要控制聊天框Item的宽度高度。  
3、需要控制聊天框ScrollView的滚动。  
4、需要控制聊天框ScrollView的伸长。  
5、需要移除历史聊天框Item。

二、左聊天框Item:  
1、创建Image作为[ChatItem_left]，设置Anchor为(left, top)、Pivot为(1, 1)。  
2、在背景下创建Image，命名为[Icon]，设置Anchor为(right, bottom)和一个Text作为[Content]。  
3、在头像下创建Text，命名为[Username]，设置Anchor为(left, middle)。  
4、挂上ChatUI脚本，专门控制UI显示。  
5、将其制作成为Prefab。

三、右聊天框Item:  
1、创建Image作为[ChatItem_right]，设置Anchor为(right, top)、Pivot为(1, 1)。  
2、在背景下创建Image，命名为[Icon]，设置Anchor为(right, bottom)和一个Text作为[Content]。  
3、在头像下创建Text，命名为[Username]，设置Anchor为(right, middle)。  
4、挂上ChatUI脚本，专门控制UI显示。  
5、将其制作成为Prefab。

四、聊天框ScrollView：  
创建一个ScrollView，设置Anchor为(stretch, stretch)，调整为适当大小。

## NO.26 Unity调用Python
[Process Class]  
Provides access to local and remote processes and enables you to start and stop local system processes.  
提供对本地和远程进程的访问，并使您能够启动和停止本地系统进程。

[ProcessStartInfo Class]  
Specifies a set of values that are used when you start a process.  
指定启动进程时使用的一组值。

---
注：  
视图有宽屏1280\*720和长屏720\*1280两种，如果视图错误，请自行调整视图！！！  
部分代码和文字来自网络，经过本人整合到本工程，有任何不明白都可以与我交流！！！  
