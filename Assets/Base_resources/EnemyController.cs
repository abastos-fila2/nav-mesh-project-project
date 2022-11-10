using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{

    public List<GameObject> puntosPatrulla;

    [SerializeField]
    [Tooltip("Rango en el que el jugador pierde")]
    float range = 5.2f;

    [SerializeField]
    NavMeshAgent agent;


    [SerializeField]
    GameObject jugador;

	private enum Estados{PERSEGUIR, PATRULLAR};
	Estados estado;

    bool attachedNavMeshAgent => agent != null;

	float dist;

    void Start()
    {
		estado = Estados.PATRULLAR;
        if (agent == null)
        {

            agent = GetComponent<NavMeshAgent>();

        }
        agent.SetDestination(puntosPatrulla[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(jugador != null){
            dist = Vector3.Distance(transform.position, jugador.transform.position);

            if(range > dist){
                Destroy(jugador);
            }
        }
        

        if (attachedNavMeshAgent)
        {
            //agent.SetDestination();
            switch(estado){
				case Estados.PATRULLAR:
					Patrullar();
					break;
				case Estados.PERSEGUIR:
					PerseguirJugador();
					break;

			}
				

			

			

        }




    }
        void Patrullar()
        {
			if(Input.GetKey(KeyCode.Space)){
				estado = Estados.PERSEGUIR;
				agent.SetDestination(jugador.transform.position);
			}
            for (int i = 0; i < puntosPatrulla.Count; i++)
            {
                print(Vector3.Distance(transform.position, puntosPatrulla[i].transform.position));
                if (Vector3.Distance(transform.position, puntosPatrulla[i].transform.position) == 0)//(transform.position.x == puntosPatrulla[i].transform.position.x && transform.position.z == puntosPatrulla[i].transform.position.z)
                {
                    if (i == puntosPatrulla.Count - 1)
                    {
                        agent.SetDestination(puntosPatrulla[0].transform.position);
                    }
                    else
                    {
                        agent.SetDestination(puntosPatrulla[i + 1].transform.position);
                    }
                }
            }
            
            // foreach(GameObject objetivo in puntosPatrulla){
            // 	if(transform.)
            // }
        }

        void PerseguirJugador()
        {
			agent.SetDestination(jugador.transform.position);
			if(Input.GetKey(KeyCode.Space)){
				estado = Estados.PATRULLAR;
				agent.SetDestination(puntosPatrulla[0].transform.position);
			}			
        }
    }