using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBehavior : MonoBehaviour
{
    //core/player control
    public float moveSpeed = 5.0f;
    public Rigidbody rigidbody;
    public GameObject body;
    private Vector3 velocity;
    private Vector3 lastLookAtDirection = Vector3.forward;


    //formation
    public enum formation
    {
        skirmishLine,
        column,
        snakeColumn,
        wedge,
        square
    }

    private formation currentFormation = formation.skirmishLine;

    public List<GameObject> socketList;
    public List<UnitMovement> unitList;

    public GameObject socket;

    public float formationScale;
    public float columnWidth;

    public bool bodyParent;
    public float formationHeight = 3.5f;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        velocity = new Vector3(Input.GetAxisRaw("MovementHorizontal"), 0, Input.GetAxisRaw("MovementVertical")).normalized * moveSpeed;
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchParent();
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentFormation = formation.skirmishLine;
            UpdateLineSockets();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentFormation != formation.column)
            {
                currentFormation = formation.column;
                UpdateColumnSockets();
            }
            else
            {
                currentFormation = formation.snakeColumn;
                UpdateColumnSockets();
            }
                
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentFormation = formation.wedge;
            UpdateWedgeSockets();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentFormation = formation.square;
            UpdateSquareSockets();
        }



        if (Input.GetKeyDown(KeyCode.P))
        {
            formationScale += 0.2f;

            switch (currentFormation)
            {
                case formation.skirmishLine:
                    UpdateLineSockets();
                    break;
                case formation.column:
                    UpdateColumnSockets();
                    break;
                case formation.wedge:
                    UpdateWedgeSockets();
                    break;
                case formation.square:
                    UpdateSquareSockets();
                    break;
                default:
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            formationScale -= 0.2f;

            switch (currentFormation)
            {
                case formation.skirmishLine:
                    UpdateLineSockets();
                    break;
                case formation.column:
                    UpdateColumnSockets();
                    break;
                case formation.wedge:
                    UpdateWedgeSockets();
                    break;
                case formation.square:
                    UpdateSquareSockets();
                    break;
                default:
                    break;
            }
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            if (currentFormation ==formation.column) columnWidth += 0.2f;
            UpdateColumnSockets();

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (currentFormation == formation.column) columnWidth -= 0.2f;
            UpdateColumnSockets();

        }


        if (currentFormation != formation.snakeColumn)
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                unitList[i].target = socketList[i].transform.position;
            }
        }
        else UpdateSnakeColumnTargets();
    }


    private void UpdateLineSockets()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            if (i % 2 == 0)//is even
            {
                if (i == 0) socketList[0].transform.localPosition = new Vector3(-formationScale, formationHeight, 0);
                else socketList[i].transform.localPosition = new Vector3((socketList[0].transform.localPosition.x - formationScale * i), formationHeight, 0);
            }
            else//is odd
            {
                if(i == 1) socketList[1].transform.localPosition = new Vector3(formationScale, formationHeight, 0);
                else socketList[i].transform.localPosition = new Vector3((socketList[1].transform.localPosition.x + formationScale * (i - 1) ), formationHeight, 0);
            }
        }
    }

    private void UpdateColumnSockets()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            if (i % 2 == 0)//is even
            {
                if (i == 0) socketList[0].transform.localPosition = new Vector3(columnWidth, formationHeight, -formationScale);
                else socketList[i].transform.localPosition = new Vector3(columnWidth, formationHeight, (socketList[0].transform.localPosition.z - (formationScale) * i));
            }
            else//is odd
            {
                if (i == 1) socketList[1].transform.localPosition = new Vector3(-columnWidth, formationHeight, -formationScale * 2);
                else socketList[i].transform.localPosition = new Vector3(-columnWidth, formationHeight, (socketList[1].transform.localPosition.z - (formationScale) * (i-1)));
            }
        }
    }

    private void UpdateSnakeColumnTargets()
    {
        if (unitList.Count > 0)
        {
            if (Vector3.Distance(transform.position, unitList[0].transform.position) > formationScale)
            {
                unitList[0].target = transform.position;
            }
            else
            {
                unitList[0].target = unitList[0].transform.position;
            }

            for (int i = 1; i < unitList.Count; i++)
            {
                if (Vector3.Distance(unitList[i - 1].transform.position, unitList[i].transform.position) > formationScale)
                {
                    unitList[i].target = unitList[i - 1].transform.position;
                }
                else
                {
                    unitList[i].target = unitList[i].transform.position;
                }


            }
        }
        
    }

    private void UpdateWedgeSockets()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            if (i % 2 == 0)//is even
            {
                if (unitList.Count == 1) socketList[0].transform.localPosition = new Vector3(0, formationHeight, formationScale);
                else socketList[i].transform.localPosition = new Vector3(-i * formationScale, formationHeight, formationScale * (unitList.Count - i));
            }
            else//is odd
            {
                socketList[i].transform.localPosition = new Vector3((i + 1) * formationScale, formationHeight, formationScale * (unitList.Count - (i+1) ) );
            }
        }
    }


    private void UpdateSquareSockets()
    {
        int layerNumber = 1;
        int j = -1;
        int k = -1;

        for (int i = 0; i < unitList.Count; i++)
        {
            while (Mathf.Abs(j) != layerNumber && Mathf.Abs(k) != layerNumber)
            {
                Debug.Log("Got unnecessary insides!");

                j++;

                if (j == layerNumber + 1)
                {
                    k++;
                    j = -layerNumber;
                }

            }


            


            socketList[i].transform.localPosition = new Vector3(formationScale * j, 3.5f, formationScale * k);//!!! Y-height is modified

            j++;

 

            

            if (j == layerNumber + 1)
            {
                Debug.Log("Next row!");
                k++;
                j = -layerNumber;
            }


            if (k == layerNumber + 1)
            {
                Debug.Log("LayerUp!");
                layerNumber++;
                j = -layerNumber;
                k = -layerNumber;
            }

        }
        
    }


    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);

        Vector3 desiredLookAtDirection;
        desiredLookAtDirection.x =  gameObject.transform.position.x + velocity.x;
        desiredLookAtDirection.z =  gameObject.transform.position.z + velocity.z;
        desiredLookAtDirection.y = 1.5f;


        if(velocity.x == 0 && velocity.z == 0)
        {
            body.transform.LookAt(lastLookAtDirection, Vector3.up);
        }
        else
        {
            body.transform.LookAt(desiredLookAtDirection, Vector3.up);
            lastLookAtDirection = desiredLookAtDirection;
        }
        
    }


    private void SwitchParent()
    {
        if (!bodyParent)
        {
            foreach (GameObject socket in socketList)
            {
                socket.transform.parent = body.transform;
            }
            bodyParent = true;
        }
        else
        {
            foreach (GameObject socket in socketList)
            {
                socket.transform.parent = transform;
            }

            bodyParent = false;
        }

    }





    public void AddAgentToSquad(UnitMovement unit)
    {
        unit.isInSquad = true;
        unitList.Add(unit);


        GameObject newSocket = Instantiate(socket);

        if (bodyParent) newSocket.transform.parent = body.transform;
        else newSocket.transform.parent = transform;

        socketList.Add(newSocket);


        switch (currentFormation)
        {
            case formation.skirmishLine:
                UpdateLineSockets();
                break;
            case formation.column:
                UpdateColumnSockets();
                break;
            case formation.snakeColumn:
                break;
            case formation.wedge:
                UpdateWedgeSockets();
                break;
            case formation.square:
                UpdateSquareSockets();
                break;
            default:
                break;
        }

        

    }



}
