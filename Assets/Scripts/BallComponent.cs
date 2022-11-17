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
        Rotate,
        getBigger,
        spinAndMoveRight
    }
    public float Speed = 1.0f;
    public List<BallInstruction> Instructions = new List<BallInstruction>();
    private int CurrentInstruction = 0;
    private float TimeInInstruction = 0.0f;
    private float InstructionLength = 0.0f;
    Vector3 myVector = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 vecRotation = Vector3.zero;
    public float TurboSpeed = 10.0f;


    void Start()
    {
        /**Debug.Log("State: " + State);
        ++State;
        Debug.Log("New State: " + State);

        **/
    }



    private void Update()
    {


        if (CurrentInstruction < Instructions.Count)
        {
            InstructionLength += Time.deltaTime;
            float RealSpeed = Speed * Time.deltaTime;

            switch (Instructions[CurrentInstruction])
            {
                case BallInstruction.MoveUp:
                    transform.position += Vector3.up * RealSpeed;
                    if (InstructionLength > 1.4f)
                    {
                        InstructionLength = 0.0f;
                        ++CurrentInstruction;
                    }
                    break;

                case BallInstruction.MoveDown:
                    transform.position += Vector3.down * RealSpeed;
                    if (InstructionLength > 1.0f)
                    {
                        InstructionLength = 0.0f;
                        ++CurrentInstruction;
                    }
                    break;

                case BallInstruction.MoveLeft:
                    transform.position += Vector3.left * RealSpeed;
                    if (InstructionLength > 0.5f)
                    {
                        InstructionLength = 0.0f;
                        ++CurrentInstruction;
                    }
                    break;

                case BallInstruction.MoveRight:
                    transform.position += Vector3.right * RealSpeed;
                    if (InstructionLength > 4.0f)
                    {
                        InstructionLength = 0.0f;
                        ++CurrentInstruction;
                    }
                    break;

                case BallInstruction.Rotate:
                    vecRotation += Vector3.forward * TurboSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);
                    if (InstructionLength > 2.0f)
                    {
                        InstructionLength = 0.0f;
                        ++CurrentInstruction;
                    }
                    break;

                case BallInstruction.getBigger:
                    transform.localScale += RealSpeed * myVector;
                    if (InstructionLength > 2.0f)
                    {
                        InstructionLength = 0.0f;
                        ++CurrentInstruction;
                    }
                    break;

                case BallInstruction.spinAndMoveRight:
                    vecRotation += Vector3.back * TurboSpeed;
                    transform.rotation = Quaternion.Euler(vecRotation);
                    transform.position += Vector3.right * RealSpeed;
                    if (InstructionLength > 5.0f)
                    {
                        InstructionLength = 0.0f;
                        ++CurrentInstruction;
                    }
                    break;

                default:
                    Debug.Log("Idle");
                    break;
            }

          
        }


        /** if (transform.localScale.x <= 3.0f)
         {
             transform.localScale += Speed * Time.deltaTime * myVector;
             
         }
        **/


    }
}
