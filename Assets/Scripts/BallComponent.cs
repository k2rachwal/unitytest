using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallComponent : MonoBehaviour
{
    public enum BallInstruction
    {
        Idle = 0,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
     // Rotate,
        getBigger,
        spinAndMoveRight
    }
    public float Speed = 1.0f;
    public List<BallInstruction> Instructions = new List<BallInstruction>();
    private int CurrentInstruction = 0;
    public float InstructionLength = 1.0f;
    Vector3 myVector = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 vecRotation = Vector3.zero;
    public float TurboSpeed = 10.0f;
    private Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 startScale = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        /**Debug.Log("State: " + State);
        ++State;
        Debug.Log("New State: " + State);

        **/
        startPosition = transform.position;
    }



    private void Update()
    {


        if (CurrentInstruction < Instructions.Count)
        {
            
           
            float RealSpeed = Speed * Time.deltaTime;

            switch (Instructions[CurrentInstruction])
            {
                case BallInstruction.MoveUp:
                    transform.position += Vector3.up * RealSpeed;
                    break;

                case BallInstruction.MoveDown:
                    transform.position += Vector3.down * RealSpeed;
                    break;

                case BallInstruction.MoveLeft:
                    transform.position += Vector3.left * RealSpeed;
                    break;

                case BallInstruction.MoveRight:
                    transform.position += Vector3.right * RealSpeed;
                    break;

                //case BallInstruction.Rotate:
                //    vecRotation += Vector3.forward * TurboSpeed;
                //    transform.rotation = Quaternion.Euler(vecRotation);
                //    break;

                case BallInstruction.getBigger:
                    transform.localScale += RealSpeed * myVector;
                    break;

                case BallInstruction.spinAndMoveRight:
                    vecRotation += Vector3.back * TurboSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);
                    transform.position += Vector3.right * RealSpeed;
                    break;

                default:
                    Debug.Log("Idle");
                    break;
            }

            if (Vector3.Distance(transform.position, startPosition) > InstructionLength)
            {
                startPosition = transform.position;
                ++CurrentInstruction;

            }
            else if ((transform.localScale.x - startScale.x) > InstructionLength)
            {
                Debug.Log("działa");
                startScale = transform.localScale;
                ++CurrentInstruction;
            }
            
        }


        /** if (transform.localScale.x <= 3.0f)
         {
             transform.localScale += Speed * Time.deltaTime * myVector;
             
         }
        **/
    
    }
}
