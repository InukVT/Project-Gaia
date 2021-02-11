using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Gaia
{
    [UpdateAfter(typeof(PlayerController))]
    public class MoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;
            

            Entities
                .ForEach((ref Translation translation, in Movement movement) =>
                {
                    var newMov = new float3
                    {
                        // X is left and right
                        x = movement.Value.x,
                        // Y is up and down
                        y = 0,
                        // Z is forwards and backwards
                        z = movement.Value.y
                    } * deltaTime * 5;

                    translation.Value += newMov;
                }).ScheduleParallel();
        }
    }
}