using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;


public class init_control : MonoBehaviour {


    public GameObject initgameobject;
    public GameObject init;

    public GameObject upper_init;
    public GameObject lower_init;
    public float speed;


    // Use this for initialization
    

    public struct rand_interval
    {
       public float Time_Line;
        public float rand_min_O;
        public float rand_max_O;

        public float rand_min_P;
        public float rand_max_P;
        public float rand_min_B;

        public float rand_max_B;
        public float target_speed;
        public float Promote_time;

    };
    /* rand_interval[] inter_table;
     rand_interval[] init_rand_interval(
     rand_interval table,
     float Time_Line_s,
     float rand_min_s,
     float rand_max_s,

     float rand_min_P_s,
     float rand_max_P_s,
     float rand_min_B_s,

     float rand_max_B_s,
     float target_speed_s,
     float Promote_time_s,
      int Cow=19){
         inter_table=new rand_interval[Cow];
         for (int i = 0; i < Cow; i++)
         {
             Time_Line_s += 10;
             inter_table[i].Time_Line = Time_Line_s;

             rand_min_s += 0;
             inter_table[i].rand_min = rand_min_s;

             rand_max_s += 0;
             inter_table[i].rand_max = rand_max_s;

             rand_min_P_s += 0;
             inter_table[i].rand_min_P = rand_min_P_s;

             rand_max_P_s += 0;
             inter_table[i].rand_max_P = rand_max_P_s;

             rand_min_B_s += -0.1f;
             inter_table[i].rand_min_B = rand_min_B_s;

             rand_max_B_s += -0.1f;
             inter_table[i].rand_max_B = rand_max_B_s;

             target_speed_s += 5;
             inter_table[i].target_speed = target_speed_s;

             Promote_time_s += 0;
             inter_table[i].Promote_time = Promote_time_s;
         }


         return inter_table;

     }
     */

    public System.Random ran;

void Start () {
        long tick = DateTime.Now.Ticks;
        ran = new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

        Read(@"ObstacleConfig.txt");
        next_Timechuo = config[r_index];
        timebk += Range(next_Timechuo.rand_min_B, next_Timechuo.rand_max_B);
    }

    void initmoster(GameObject upper,GameObject lower,float speed)
    {
        init = GameObject.Instantiate(initgameobject, initgameobject.transform.position, initgameobject.transform.rotation);
        init.AddComponent<tag_destroy>();
        init.gameObject.GetComponent<updateposition>().uppermodel = upper;
        init.gameObject.GetComponent<updateposition>().lowermodel = lower;
        init.gameObject.GetComponent<updateposition>().OMEGA = speed;
        init.gameObject.GetComponent<updateposition>().enabled = true;

    }           //同时上下生成两个敌人  接口
    
    

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp (KeyCode.Space))
        {
            initmoster(upper_init,lower_init,speed);
        }
        timeline += Time.deltaTime;
        Check();
	}

    //导入配置表
   
    List<rand_interval> config = new List<rand_interval>();
    public void Read(string path)
    {
        StreamReader sr = new StreamReader(path, Encoding.Default);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string sline = line.ToString();
            if(sline.Length>20&&sline.Length<40)
            {
                rand_interval r_temp = new rand_interval();
                string[] ls_temp = sline.Split('\t');
                r_temp.Time_Line =(float) Convert.ToDouble (ls_temp[0]);
                r_temp.rand_min_O = (float)Convert.ToDouble(ls_temp[1]);
                r_temp.rand_max_O = (float)Convert.ToDouble(ls_temp[2]);
                r_temp.rand_min_P= (float)Convert.ToDouble(ls_temp[3]);
                r_temp.rand_max_P = (float)Convert.ToDouble(ls_temp[4]);
                r_temp.rand_min_B = (float)Convert.ToDouble(ls_temp[5]);
                r_temp.rand_max_B= (float)Convert.ToDouble(ls_temp[6]);
                r_temp.target_speed = (float)Convert.ToDouble(ls_temp[7]);
                r_temp.Promote_time = (float)Convert.ToDouble(ls_temp[8]);
                config.Add(r_temp);
 
            }
        }
    } 


    //同一时间出现随机2个不同敌人的简化版时间生成
    public float timeline=0f;
    rand_interval next_Timechuo;            //下一个需要执行的时间节点
    private int r_index = 0;
    private float timebk = 0;
    private void Check()
    {
      //  timebk = next_Timechuo.Time_Line + Range(next_Timechuo.rand_min_O, next_Timechuo.rand_max_O);
        if (timebk <timeline)
        {
            //敌人生成
            
            if(DateTime.Now.Millisecond%2==1)
            {
                initmoster(upper_init, lower_init, speed*(Range(next_Timechuo.rand_min_O, next_Timechuo.rand_max_O) + (next_Timechuo.target_speed/100)));
            }
            else {
            initmoster(lower_init, upper_init, speed * (Range(next_Timechuo.rand_min_O, next_Timechuo.rand_max_O) + (next_Timechuo.target_speed / 100)));
            }

            timebk += Range(next_Timechuo.rand_min_B, next_Timechuo.rand_max_B);
           // initmoster(GameObject upper, GameObject lower, float speed)
           //更新时间戳
           if(timebk> config[r_index + 1].Time_Line)
            {
                next_Timechuo = config[r_index+1];
                r_index++;
            }
  
        }
    }                  
   private float Range(float min,float max)
    {
        float res = min + (max - min )* (float)ran.NextDouble();
        return res;
    }
}
