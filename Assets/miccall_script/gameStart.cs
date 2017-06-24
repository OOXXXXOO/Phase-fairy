using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluffyUnderware.DevTools;
using UnityEngine.UI;
namespace FluffyUnderware.Curvy.Controllers
{
    public class gameStart : MonoBehaviour
    {

        //旋转速度
        public float speed = 7f ;
        private float firstspeed = 7f;
        //停止加速度
        public float Acceleration_stop = 20f;
        //两个player
        private GameObject player_1;
        private GameObject player_2;
        public GameObject player_1_prefab;
        public GameObject player_2_prefab;
        //路径 
        public GameObject player_1_path_prefab;
        public GameObject player_2_path_prefab;
        private GameObject player_1_path;
        private GameObject player_2_path;

        //音效
        public GameObject audio_source;

        //摄像机
        public camview camview ;
        public CamviewEnd camviewEnd;
        //路径控制脚本 
        private  SplineController   pc_1;
        private  SplineController  pc_2;
        //????
        private float currentpotion_play_1 ;
        private float currentpotion_play_2;
        
        //是否游戏开始 
        private bool isstart = false;
        //停止时间 
        private float stoptime ;

        int flag = 0;

        // 游戏物体 
        public GameObject juese01;
        public GameObject juese02;
        public init_control init_c;
        public GameObject UI_pan;
        public GameObject UI_score;


        // Use this for initialization
        void Start()
        {

            initGame();
            initSetting();

            
            if (GameObject.Find("Auido(Clone)") == null) {
                var audio = Instantiate(audio_source, Vector3.zero, Quaternion.identity);
                DontDestroyOnLoad(audio);
            }
            
            
        }

        // Update is called once per frame
        void Update()
        {
            
            //按钮按下 开始游戏的预设 
            if (isstart)
            {
                //获取 结束 加速度  
                getStopAcc();

                //相机缩放

                camview.enabled = true ;
                camviewEnd.enabled = false;
                //结束场景循环 
                pc_1.Clamping = CurvyClamping.Clamp;
                pc_2.Clamping = CurvyClamping.Clamp;
             
                if( player_1.transform.position.y == 0.5 )
                {
                    LoadScript();
                }
                //异常数据处理
                AbnormalDataProcessing();
                //分数显示
                scoreUI();
            }
            else
            {
                //Todo: 归位
                camview.enabled = false;
            }
            
        }


        void AbnormalDataProcessing()
        {
            if (speed >= 2)
            {
                pc_1.Speed = speed;
                pc_2.Speed = speed;
            }
            else
            {
                pc_1.Speed = 3f;
                pc_2.Speed = 3f;
            }
        }

        public void onStartClick()
        {
            isstart = true;
        }


        public void ongameend()
        {
            isstart = false;
        }



        private void getStopAcc()
        {
            if ((1 - pc_1.RelativePosition) > 0.15f)
                speed = Mathf.Lerp(speed, 20f, Acceleration_stop);
            else
                speed = Mathf.Lerp(speed, 0, Acceleration_stop);
        }


        private void initGame()
        {

            //实例化player
            if (player_1 == null)
                player_1 = Instantiate(player_1_prefab,new Vector3(0f,0f,0f),new Quaternion());
            if (player_2 == null)
                player_2 = Instantiate(player_2_prefab, new Vector3(0f, 0f, 0f), new Quaternion());
            //实例化路径
            if(player_1_path == null )
                player_1_path = Instantiate(player_1_path_prefab, new Vector3(0f, 0f, -2f), new Quaternion());
                player_1_path.transform.eulerAngles = new Vector3(0,0,180);
            if (player_2_path == null)
                player_2_path = Instantiate(player_2_path_prefab, new Vector3(0f, 0f, -2f), new Quaternion());

            //绑定
            player_1.GetComponent<SplineController>().Spline = player_1_path.GetComponent<CurvySpline>();
            player_2.GetComponent<SplineController>().Spline = player_2_path.GetComponent<CurvySpline>();


            //设置UI为false
            UI_score.SetActive(false);


        }

        private void initSetting()
        {
            //获取控制器
            pc_1 = player_1.GetComponent<SplineController>();
            pc_2 = player_2.GetComponent<SplineController>();
            //设置速度
            pc_1.Speed = speed;
            pc_2.Speed = speed;

            //other setting 

        }


        //游戏结束调用
        public void gameover()
        {
            //销毁物体
            Destroy(player_1);
            Destroy(player_2);
            Destroy(player_1_path);
            Destroy(player_2_path);
            player_1 = null;
            player_2 = null;
            player_1_path = null;
            player_2_path = null;
            UI_pan.SetActive(true);
            //设置UI为true
            UI_score.SetActive(false);
            //结束当前
            ongameend();
            //相机拉远
            camview.resetCam();
            camview.enabled = false;
            camviewEnd.enabled = true;
            //控制UI OnGameEnd
            transform.GetComponent<UIcontroller>().ongameend();
            //初始化速度
            speed = firstspeed;
            //重新开始游戏
            this.Start();
        }

        public void LoadScript()
        {
            startnewScript();
            player_1.transform.position = new Vector3(0f,0.5f,-2f);

            player_1.SetActive(false);
            juese01.SetActive(true);
            player_2.SetActive(false);
            juese02.SetActive(true);

            init_c.enabled = true;
            UI_pan.SetActive(false);
            //设置UI为false
            UI_score.SetActive(true);


        }
        public void startnewScript()
        {
            player_1.GetComponent<SplineController>().enabled = false;
            player_2.GetComponent<SplineController>().enabled = false;

            player_1.GetComponent<moniwalle>().enabled = true;
            player_2.GetComponent<moniwalle>().enabled = true;

        }

        private void scoreUI()
        {
            Text score_texet = UI_score.GetComponentInChildren<Text>();
            int ss = (int)juese01.GetComponent<w_score>().wscore;
            string s = ss.ToString();
            score_texet.text = s;
        }



    }
}
