//using UnityEngine;

using MessagePack;


namespace ServerShared.MessagePackObjects
{
    /// <summary>
    /// Room participation information
    /// </summary>
    [MessagePackObject]
    public struct JoinRequest
    {
        [Key(0)]
        public string RoomName { get; set; }

        [Key(1)]
        public string UserName { get; set; }

        [Key(2)]
        public int UserCount { get; set; }
/*
        [Key(3)]
        public Vector3 Position { get; set; }

        [Key(4)]
        public Quaternion Rotation { get; set; }

        [Key(5)]
        public Vector3 Scale { get; set; }
*/
    }
}
