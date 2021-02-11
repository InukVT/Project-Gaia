#nullable enable
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Gaia
{
    public class PlayerController : SystemBase
    {
        private Input input = new Input();
        private float2? vector;

        protected override void OnCreate()
        {
            input.Player.Move.performed += Move;
            input.Player.Move.canceled += StopMove;
        }

        protected override void OnStartRunning()
            => input.Enable();

        protected override void OnStopRunning()
            => input.Disable();

        protected override void OnUpdate()
        {
            var move = vector ?? float2.zero;

            Entities
                .WithAll<PlayerTag>()
                .ForEach((ref Movement playerMove) => {
                    Debug.Log($"Moving {move}");
                    playerMove = move;
                    Debug.Log($"Moded {playerMove}");
            }).Run();
        }

        private void Move(InputAction.CallbackContext callback)
        {
           
            vector = callback.ReadValue<Vector2>();
        }

        private void StopMove(InputAction.CallbackContext callback)
        {
            vector = null;
        }
    }
    /*
    [UpdateAfter(typeof(PlayerController))]
    public class MoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;

            Entities
                .ForEach((ref Translation translatopm, in Movement movement) =>
                {
                    translation.Value += movement.Value * deltaTime;
                }).ScheduleParallel();
        }
    }*/
}