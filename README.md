# 3D Game Programming & Design：游戏智能

# 游戏智能概述
游戏智能是指：
> 在游戏规则约束下，通过适当的 算法 使得游戏中 NPC（Non-Player Character） 呈现为具有一定人类智能行为的 博弈对手，让游戏玩家面临不间断的挑战，并在挑战中有所收获，包括知识和技能等。

游戏智能需要具备以下几个性质：

 - 拟人化。游戏智能更注重游戏对象行为结果类似（模拟）人，既能做出令人惊讶的有效行为，也会犯各种愚笨的错误，从而与现实世界中不同的人匹配。而人工智能通常是超人设计，追求最佳结果。
 - 可玩性。游戏智能并不意味着高大上的算法，它更注重针对不同类型的玩家设计不同能力的 NPC，例如：“小怪物”，“大boss”等等，通常用等级这个参数来表达游戏智能 agent 的能力。
 - 趣味性。游戏智能设计注重娱乐，而不只是算法研究。因此，算法会集合一些特效，让人感到愉悦。

# 模型、方法与常用算法
**“感知-思考-行为”模型**

“sense-think-act” paradigm（范式） 是构造 agent、robot、NPC（Non-Player Character） 的基础概念。自从上世纪80年代提出以来，我们使用 Sense-Think-Act 范例思考机器人如何工作，并设计它们。 

**1、感知（Sense）**

感知是 agent 接收世界信息的行为，其获取的数据将是思考的输入。这里主要要考虑的问题是如何限制信息获取是设计不同级别 agent 的核心问题。

在游戏中，定义获取信息能力通常可以从视觉、听觉和嗅觉等渠道去考虑：

 - 视觉（Vision） 
 
 	识别“敌人”的位置和属性 识别“障碍物”及其范围 
 - 听觉（Sound） 
 	
 	识别事件的方向和距离 
 - 嗅觉（Smell）
   
   获得玩家/事件的痕迹

使用距离、角度、障碍物限制 agent 发现玩家位置等信息，或者使用干扰影响信息的准确性，这些是设计游戏 agent 常见的手段。如果 agent 都拥有玩家位置和导航地图，结果就是一窝蜂的现象，这是没有趣的事情。合适的限制，将使得 agent 在相同的决策算法下，呈现丰富的行为。

一种简单的方法就是 agent 结合不同类型的触发器、探头，构造不同的获取信息的场景。最后将感知的结果放在一个数据结构中（类似 UnityEngine.EventSystems.PointerEventData）

 **在 Agent Thinking 时，不可获取感知以外的数据信息。**

**2、思考（Think）**

Think 就是算法，它的输入是感知的数据，输出是行为（behaviours）。 思考的算法，通常就是我们所说的游戏规则的一部分，即 agent 能做什么，该做什么。 游戏 agent 的思考类似人脑的决策过程，建立符合游戏玩家难度曲线，可以控制、且符合社会准则的行为。另一个相关问题，玩家难度曲线在编程阶段是未知的，依赖众多玩家与 agent 的操作与对抗结果。它在游戏测试和运维过程中存在巨大不确定性。因此将 agent 决策过程用 If … Then … 硬编码写入程序逻辑是不可行的！

目前主要的方法有：

**规则推理引擎**

规则推理引擎（Rule-based Inference Engine）也叫产生式系统或推理模型（inference model），是由基于规则的专家系统发展而来。基于规则的专家系统，基本概念包括：

 - 事实（facts）: 事实是用来表示已知的数据或信息。 
 - 规则（Rules）: 即产生式规则，用来表示系统推理的有关知识。
   规则由条件和动作组成，格式一般为：IF 条件 Then 动作，例如 Rule1: Human(x) => Mortal(x)
   （一阶谓词逻辑，离散数学）

**状态机引擎**

有限状态自动机（Finite State Machine / FSM）是可以图形化的自动执行工具。在 unity 中，它是标准化的 agent（NPC） 动作自动控制工具。定义一个状态机主要工作包括：

- 状态：该组件定义了一组状态，一个游戏实体或NPC可以选择（巡逻、追逐和射击）
- 转移：该组件定义了不同状态之间的关系
- 规则：该组件用来触发状态转移（玩家在视线范围内、距离足够攻击、丢失/杀死玩家）
- 事件：该组件用于触发检查规则（守卫的可见区域、与玩家的距离等）

**决策树**

决策树（Decision Trees）又称为行为树（Behaviour Trees）。

**3、行动（Act）**

行动（Act）将思考（Think）的结果作为输入，该部分的任务就是使得 agent 行为更符合物理世界的规律，使得“心想事成”这样理想的结果变得不确定。

通常需要考虑的要素包括：

- 准备时间。在准备时间通过光、声等方式提示对手
- 动作时间。从动作开始到结束是一个序列
- 干扰因素。利用风、地形、随机数使炮弹有一定偏差

# Unity 3D 导航与寻路
Unity 导航系统允许创建给游戏角色导航的游戏世界。游戏角色可以在蓝色联通的网格上，找到去任意一点最短的路径，且具有一定爬坡、跳沟壑的能力。
- **NavMesh** (Navigation Mesh) 是一种数据结构，它描述了游戏对象可行走的表面。通过三角网格，计算其中任意两点之间的最短路径，用于游戏对象的导航。它是根据场景几何结构自动创建或烘焙构建。
- **NavMesh Agent**组件创建具有寻路能力的角色。Agent 使用NavMesh 推理，避免彼此以及移动障碍物。
- **Off-Mesh** Link组件允许将不连接的块之间建立“传送门”。例如，跳过沟渠或围栏，或在穿过它之前打开门，都可以被描述为 Off-Mesh Link。
- **NavMesh 障碍** 组件允许您描述 agent 在移动时应避免的移动障碍。由物理系统控制的桶或箱子就是很典型的障碍。在障碍物移动的过程中，Agent 尽力避开它，但一旦障碍物变得静止，它将在导航网格上开一个洞，以便 Agent 可以改变他们的路径以绕过它，或者静止的障碍物阻塞路径，使得 Agent 找到其他路线。

# 编程实践
## 作业要求
本次作业基本要求是三选一，我选择的是第二个：坦克对战游戏 AI 设计。

具体如下：

从商店下载游戏：“Kawaii” Tank 或 其他坦克模型，构建 AI 对战坦克。具体要求

- 使用“感知-思考-行为”模型，建模 AI 坦克
- 场景中要放置一些障碍阻挡对手视线
- 坦克需要放置一个矩阵包围盒触发器，以保证 AI 坦克能使用射线探测对手方位
- AI 坦克必须在有目标条件下使用导航，并能绕过障碍。（失去目标时策略自己思考）
- 实现人机对战

## 
本次作业参考和使用了Unity3D官方教程的Tanks! Tutorial里面的模型进行构建。同时参考了博客[Unity 3D官方教程——Tanks!学习记录](https://blog.csdn.net/wocaishiaige/article/details/65217751)来对各项参数进行配置。

## 代码分析
添加文本实现坦克的各种运动，打开Scripts下Tank下TankMovement脚本并编辑。这里实现了血量的初始化和控制坦克在被子弹击中后的血量变化以及发射子弹的操作。

```javascript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    private float hp =1000.0f;
    // 初始化
    public Tank()
    {
        hp = 1000.0f;
    }

    public float getHP()
    {
        return hp;
    }

    public void setHP(float hp)
    {
        this.hp = hp;
    }

    public void beShooted()
    {
        hp -= 100;
    }
    
    public void shoot(TankType type)
    {
        GameObject bullet = Singleton<MyFactory>.Instance.getBullets(type);
        bullet.transform.position = new Vector3(transform.position.x, 2.0f, transform.position.z) + transform.forward * 2.0f;
        bullet.transform.forward = transform.forward; //方向
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
    }
}

```
AI坦克也就是敌方坦克是我们本次实验的一个重点，这里我们就是利用的前面几个部分所讲的原则和技术来实现使NPC自动巡航定位玩家的位置并且自动向玩家位置靠近。

导航利用的是NavMeshAgent agent组件创建具有寻路能力的AI坦克。Agent 使用NavMesh 推理，能够避免彼此碰撞以及移动障碍物等操作。此外，我们利用协程来实现自动射击，当距离玩家距离在10以内时，就会发射子弹。

```javascript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Tank {
    public delegate void RecycleEnemy(GameObject enemy);
    //当enemy被摧毁时，通知工厂回收；
    public static event RecycleEnemy recycleEnemy;
    // player 的位置
    private Vector3 playerLocation;
    private bool gameover;
    private void Start()
    {
        playerLocation = GameDirector.getInstance().currentSceneController.getPlayer().transform.position;
        StartCoroutine(shoot());
    }

    void Update() {
        playerLocation = GameDirector.getInstance().currentSceneController.getPlayer().transform.position;
        gameover = GameDirector.getInstance().currentSceneController.getGameOver();
        if (!gameover)
        {
            if (getHP() <= 0 && recycleEnemy != null)
            {
                recycleEnemy(this.gameObject);
            }
            else
            {
                // 自动向player移动
                NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
                agent.SetDestination(playerLocation);
            }
        }
        else
        {
            //游戏结束，停止寻路
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }
    // 协程实现每隔1s进行射击，开始喜欢协程了
    IEnumerator shoot()
    {
        while (!gameover)
        {
            for(float i =1;i> 0; i -= Time.deltaTime)
            {
                yield return 0;
            }
            if(Vector3.Distance(playerLocation,gameObject.transform.position) < 10)
            {
                shoot(TankType.ENEMY);
            }
        }
    }
}

```

玩家坦克是对坦克类的继承，因为玩家是需要我们控制它的射击，方向，以及移动的，所以这里主要是在实现这些功能。

控制前进后退的分别是W／S，控制左右方向旋转的是A／D
```javascript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank{
    public delegate void DestroyPlayer(); // game over!
    public static event DestroyPlayer destroyEvent;
    void Start () {
        setHP(1000);
	}
	
	// Update is called once per frame
	void Update () {
		if(getHP() <= 0)    // game over!
        {
            this.gameObject.SetActive(false);
            destroyEvent();
        }
	}

    //键盘w，s控制前后移动
    public void moveForward()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 50;
    }
    public void moveBackWard()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * -50;
    }

    //键盘a，d控制原地左右旋转的方向。通过水平轴上的增量，改变玩家坦克的欧拉角，从而实现坦克转向
    public void turn(float offsetX)
    {
        float x = gameObject.transform.localEulerAngles.x;
        float y = gameObject.transform.localEulerAngles.y + offsetX*2;
        gameObject.transform.localEulerAngles = new Vector3(x, y, 0);
    }
}

```
子弹类首先是要区分出攻击的是敌方还是己方，所以我们先对坦克类型做一个判断，如果是敌方才会攻击，否则就不会射击。

同时为了让游戏的可玩性更强，我们设置了玩家的攻击力度要比AI坦克更强，降低了游戏难度。

```javascript
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float explosionRadius = 3.0f;
    private TankType tankType;

    //设置发射子弹的坦克类型
    public void setTankType(TankType type)
    {
        tankType = type;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.tag == "tankEnemy" && this.tankType == TankType.ENEMY ||
            collision.transform.gameObject.tag == "tankPlayer" && this.tankType == TankType.PLAYER)
        {
            return;
        }
        MyFactory factory = Singleton<MyFactory>.Instance;
        ParticleSystem explosion = factory.getParticleSystem();
        explosion.transform.position = gameObject.transform.position;
        //获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius);
        
        foreach(var collider in colliders)
        {
            //被击中坦克与爆炸中心的距离
            float distance = Vector3.Distance(collider.transform.position, gameObject.transform.position);
            float hurt;
            // 如果是玩家发出的子弹伤害高一点
            if (collider.tag == "tankEnemy" && this.tankType == TankType.PLAYER)
            {
                hurt = 300.0f / distance;
                collider.GetComponent<Tank>().setHP(collider.GetComponent<Tank>().getHP() - hurt);
            }
            else if(collider.tag == "tankPlayer" && this.tankType == TankType.ENEMY)
            {
                hurt = 100.0f / distance;
                collider.GetComponent<Tank>().setHP(collider.GetComponent<Tank>().getHP() - hurt);
            }
            explosion.Play();
        }

        if (gameObject.activeSelf)
        {
            factory.recycleBullet(gameObject);
        }
    }

}

```
然后工厂类就是生产和回收上述角色，这个在前面我们经常用到。场景类就是一个实例化的过程，进行初始化和实现UI类的操作。UI类主要是和用户的交互，检测玩家输入和输出游戏结果等。因为在之前的作业中这些都已经涉及到过了，所以这里就不再代码分析啦，详情看GitHub上的代码吧。

## 游戏界面效果
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191205185955987.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDM3NzY5MQ==,size_16,color_FFFFFF,t_70)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191205190026117.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDM3NzY5MQ==,size_16,color_FFFFFF,t_70)![在这里插入图片描述](https://img-blog.csdnimg.cn/2019120519004624.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L3dlaXhpbl80MDM3NzY5MQ==,size_16,color_FFFFFF,t_70)


- 最后
[视频链接](https://pan.baidu.com/s/1BCr9AJ_EEf_fgMw5OAIhAQ)

