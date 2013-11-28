using UnityEngine;
using System.Collections;

public class DynamicChickenCreation : MonoBehaviour {
        
        private int Chicken, Team;//Chicken means the chicken model's choice at StartGame scene, and Team the team's choice
        private int enemyTeam;
        //private ArrayList TeamList = new ArrayList();//the first element at this list will be the team chosen at StartGame scene
        void Start () {//at start the scene
                
                Chicken = PlayerPrefs.GetInt("Chicken");//we get the values from the startGame scene through PlayerPrefs
                Team = PlayerPrefs.GetInt("Team");
                
                //Chicken = 1;
                //Team = Actor.ROBOT_TEAM;
                
                setPlayer();
                setAllyTeam();

                
                setEnemyTeam();
        }
        
        //This function sets the player inside the scene
        void setPlayer(){
                GameObject ob = CreateChicken(Team,Chicken);        
                initAsPlayer(ob);
                ob.GetComponent<PlayerController>().setTeam(Team);
                ob.GetComponent<PlayerController>().setWeapon(Actor.WEAPON_KATANA);
        }
        
        void setEnemyTeam(){
                /*
                 * CREATE HERE THE ENEMY TEAM DINAMICALLY
                */
                
                if(Team == 1)
                        enemyTeam = 2;
                else
                        enemyTeam = 1;
                
                for(int i=1; i<6; i++){
                        GameObject ob = CreateChicken(enemyTeam,i);
                        initAsNPC(ob,enemyTeam,i);        
                        ob.GetComponent<AgentNpc>().setTeam(enemyTeam);
                        ob.tag = "NPC";
                }
        }

        void setAllyTeam(){
                /*
                 * CREATE HERE THE ALLY TEAM DINAMICALLY
                */
                for(int i=1; i<6; i++){
                        if (i==Chicken) continue;
                        GameObject ob = CreateChicken(Team,i);        
                        initAsNPC(ob,Team,i);
                        ob.GetComponent<AgentNpc>().setTeam(Team);
                        ob.tag = "Allied";
                }
        }
        
        
        /*NOTE: Check out which chicken number is related to the choose menu*/
        /* Example code:
         *
         * GameObject c = Instantiate(Resource.Load("ChickenPrefabs/RoboChicken5")) as GameObject;//this means the prefabs must be at the Resources/chickenPrefabs folder
         *
         *
         * */
        
        
        GameObject setRoboChicken1(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/robo_droid")) as GameObject;
                c.name = "Robot1";
                return c;
        }
        
        GameObject setRoboChicken2(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/robo_droid")) as GameObject;
                c.name = "Robot2";
                return c;
        }
        
        GameObject setRoboChicken3(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/robo_chicken")) as GameObject;
                c.name = "Robot3";
                return c;
        }
        
        GameObject setRoboChicken4(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/robo_droid")) as GameObject;
                c.name = "Robot4";
                return c;
        }
        
        GameObject setRoboChicken5(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/robo_chicken")) as GameObject;
                c.name = "Robot5";
                return c;
        }
        
        GameObject setPhiloChicken1(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_leader")) as GameObject;
                c.name = "Philo1";
                return c;
        }
        
        GameObject setPhiloChicken2(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_crazy")) as GameObject;
                c.name = "Philo2";
                return c;
        }
        
        GameObject setPhiloChicken3(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_graduated")) as GameObject;
                c.name = "Philo3";
                return c;
        }
        
        GameObject setPhiloChicken4(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_4eyes")) as GameObject;
                c.name = "Philo4";
                return c;
        }
        
        GameObject setPhiloChicken5(){
                GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_leader")) as GameObject;
                c.name = "Philo5";
                return c;
        }
        
        
        void initAsPlayer(GameObject c){
                c.AddComponent<PlayerController>();
                c.tag = "Player";
                if(Team == 2)
                        c.transform.position = new Vector3(-538.14F,-78.06F,0.8170023F);
                else
                        c.transform.position = new Vector3(15700.14F,-78.06F,0.8170023F);
                
                
                // Y mas cosas
        }
        
        private int alliedNum = 1;
        void initAsNPC(GameObject c,int Team, int chicken){
                if (Team == 2){
                        switch(chicken){
                                case 1:
                                        c.transform.position = new Vector3(-44.11278f,-80.28318f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaAliada1";
										c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_KATANA;
                                        break;
                                case 2:
                                        c.transform.position = new Vector3(494.1796f,-80.28318f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaAliada2";
										c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_KATANA;
                                        break;
                                case 3:
                                        c.transform.position = new Vector3(549.9143f,245.3285f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaAliada3";
										c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_PISTOLA;
                                        break;
                                default: // case 4
                                        c.transform.position = new Vector3(135.0389f,77.73074f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaAliada4";
										c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_ESCOPETA;
                                        break;
                        }
                        alliedNum++;
                        
                }else{
                        switch(chicken){
                                case 1:
                                        c.transform.position = new Vector3(12000.105f,-91.74977f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaRival1";
                                        c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_PISTOLA;
                                        break;
                                case 2:
                                        c.transform.position = new Vector3(13000.58f,118.5825f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaRival2";
                                        c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_KATANA;
                                        break;
                                case 3:
                                        c.transform.position = new Vector3(15700.028f,-81.72346f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaRival3";
                                        c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_PISTOLA;
                                        break;
                                case 4:
                                        c.transform.position = new Vector3(13051.11f,118.5825f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaRival4";
                                        c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_KATANA;
                                        break;
                                default: // case 5
                                        c.transform.position = new Vector3(14806.89f,414.3137f,0.8170023f);
                                        c.AddComponent<AgentNpc>().direrutas = "rutaRival5_Agresiva";
                                        c.GetComponent<AgentNpc>().weapon = Actor.WEAPON_ESCOPETA;
                                        break;
                        }
                }
        }
        
        GameObject CreateChicken(int team,int chickenNum){
                
                GameObject chicken = null;
                
                if(team == Actor.PHILO_TEAM){
                        switch(chickenNum){
                                case 1:
                                        chicken = setPhiloChicken1();
                                break;
                                
                                case 2:
                                        chicken = setPhiloChicken2();
                                break;
                                
                                case 3:
                                        chicken = setPhiloChicken3();
                                break;
                                
                                case 4:
                                        chicken = setPhiloChicken4();
                                break;
                                
                                case 5:
                                        chicken = setPhiloChicken5();
                                break;
                                
                        
                        }
                }else if(team == Actor.ROBOT_TEAM){
                        switch(chickenNum){
                                case 1:
                                        chicken = setRoboChicken1();
                                break;
                                
                                case 2:
                                        chicken = setRoboChicken2();
                                break;
                                
                                case 3:
                                        chicken = setRoboChicken3();
                                break;
                                
                                case 4:
                                        chicken = setRoboChicken4();
                                break;
                                
                                case 5:
                                        chicken = setRoboChicken5();
                                break;
                                
                        
                        }
                }
                return chicken;
        }
        
}
