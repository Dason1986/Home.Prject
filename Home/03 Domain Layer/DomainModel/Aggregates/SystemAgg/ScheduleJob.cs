using Library.ComponentModel.Model;
using Library.Domain;
using System.ComponentModel;

namespace Home.DomainModel.Aggregates.SystemAgg
{  /// <summary>
   ///排程設置表
   /// </summary>
    [DisplayName("排程設置表"), Description("排程設置表")]
    public class ScheduleJob : AuditedEntity
    {
        public ScheduleJob(ICreatedInfo clientHeaderManaegment)
            : base(clientHeaderManaegment)
        {
        }

        public ScheduleJob()
        {
        }

        /// <summary>
        /// 標題
        /// </summary>
        [Description(@"標題")]
        public string Title { get; set; }

        /// <summary>
        /// 執行類
        /// </summary>
        [Description(@"執行類")]
        public string ScheduleJobClass { get; set; }

   



        /*
 一个cron表达式有至少6个（也可能7个）有空格分隔的时间元素。
 按顺序依次为
 1.秒（0~59）
 2.分钟（0~59）
 3.小时（0~23）
 4.天（月）（0~31，但是你需要考虑你月的天数）
 5.月（0~11）
 6.周（星期）（1~7 1=SUN 或 SUN，MON，TUE，WED，THU，FRI，SAT）
 7.年份（1970－2099）

其中每个元素可以是
一个值(如6),
一个连续区间(9-12),
一个间隔时间(8-18/4)(/表示每隔4小时),
一个列表(1,3,5),通配符。由于"月份中的日期"和"星期中的日期"这两个元素互斥的,必须要对其中一个设置?.

0 0 10,14,16 * * ?      每天上午10点，下午2点，4点
0 0/30 9-17 * * ?     朝九晚五工作时间内每半小时
0 0 12 ? * WED 表示每个星期三中午12点

有些子表达式能包含一些范围或列表例如：
子表达式（天（星期））可以为 “MON-FRI”，“MON，WED，FRI”，“MON-WED,SAT”
“*”字符代表所有可能的值因此，“*”在子表达式（月）里表示每个月的含义，“*”在子表达式（天（星期））表示星期的每一天
“/”字符用来指定数值的增量例如：在子表达式（分钟）里的“0/15”表示从第0分钟开始，每15分钟 ;在子表达式（分钟）里的“3/20”表示从第3分钟开始，每20分钟（它和“3，23，43”）的含义一样“？”字符仅被用于天（月）和天（星期）两个子表达式，表示不指定值当2个子表达式其中之一被指定了值以后，为了避免冲突，需要将另一个子表达式的值设为“？”

“L” 字符仅被用于天（月）和天（星期）两个子表达式，它是单词“last”的缩写

但是它在两个子表达式里的含义是不同的。

在天（月）子表达式中，“L”表示一个月的最后一天 ,在天（星期）自表达式中，“L”表示一个星期的最后一天，也就是SAT

如果在“L”前有具体的内容，它就具有其他的含义了

例如：“6L”表示这个月的倒数第６天，“ＦＲＩＬ”表示这个月的最后一个星期五

注意：在使用“L”参数时，不要指定列表或范围，因为这会导致问题

============================================

CronTrigger配置完整格式为： [秒] [分] [小时] [日] [月] [周] [年]
             */
        /// <summary>
        /// 執行時間XML
        /// </summary>
        [Description(@"執行時間的條件")]
        public string ScheduleCronExpression { get; set; }




    }
}