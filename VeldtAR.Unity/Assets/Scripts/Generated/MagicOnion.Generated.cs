// <auto-generated />
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::MagicOnion;
    using global::MagicOnion.Client;

    public static partial class MagicOnionInitializer
    {
        static bool isRegistered = false;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if(isRegistered) return;
            isRegistered = true;

            MagicOnionClientRegistry<ServerShared.Services.IVeldtService>.Register((x, y, z) => new ServerShared.Services.VeldtServiceClient(x, y, z));

            StreamingHubClientRegistry<ServerShared.Hubs.IVeldtHub, ServerShared.Hubs.IVeldtHubReceiver>.Register((a, _, b, c, d, e) => new ServerShared.Hubs.VeldtHubClient(a, b, c, d, e));
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion.Resolvers
{
    using System;
    using MessagePack;

    public class MagicOnionResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new MagicOnionResolver();

        MagicOnionResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = MagicOnionResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class MagicOnionResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static MagicOnionResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(3)
            {
                {typeof(global::MagicOnion.DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>), 0 },
                {typeof(global::System.Collections.Generic.Dictionary<int, string>), 1 },
                {typeof(global::System.Collections.Generic.List<int>), 2 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new global::MagicOnion.DynamicArgumentTupleFormatter<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>(default(global::System.Collections.Generic.List<int>), default(global::System.Collections.Generic.Dictionary<int, string>));
                case 1: return new global::MessagePack.Formatters.DictionaryFormatter<int, string>();
                case 2: return new global::MessagePack.Formatters.ListFormatter<int>();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace ServerShared.Services {
    using System;
    using MagicOnion;
    using MagicOnion.Client;
    using Grpc.Core;
    using MessagePack;

    [Ignore]
    public class VeldtServiceClient : MagicOnionClientBase<global::ServerShared.Services.IVeldtService>, global::ServerShared.Services.IVeldtService
    {
        static readonly Method<byte[], byte[]> GenerateExceptionMethod;
        static readonly Func<RequestContext, ResponseContext> GenerateExceptionDelegate;
        static readonly Method<byte[], byte[]> SendReportAsyncMethod;
        static readonly Func<RequestContext, ResponseContext> SendReportAsyncDelegate;

        static VeldtServiceClient()
        {
            GenerateExceptionMethod = new Method<byte[], byte[]>(MethodType.Unary, "IVeldtService", "GenerateException", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            GenerateExceptionDelegate = _GenerateException;
            SendReportAsyncMethod = new Method<byte[], byte[]>(MethodType.Unary, "IVeldtService", "SendReportAsync", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            SendReportAsyncDelegate = _SendReportAsync;
        }

        VeldtServiceClient()
        {
        }

        public VeldtServiceClient(CallInvoker callInvoker, MessagePackSerializerOptions serializerOptions, IClientFilter[] filters)
            : base(callInvoker, serializerOptions, filters)
        {
        }

        protected override MagicOnionClientBase<IVeldtService> Clone()
        {
            var clone = new VeldtServiceClient();
            clone.host = this.host;
            clone.option = this.option;
            clone.callInvoker = this.callInvoker;
            clone.serializerOptions = this.serializerOptions;
            clone.filters = filters;
            return clone;
        }

        public new IVeldtService WithHeaders(Metadata headers)
        {
            return base.WithHeaders(headers);
        }

        public new IVeldtService WithCancellationToken(System.Threading.CancellationToken cancellationToken)
        {
            return base.WithCancellationToken(cancellationToken);
        }

        public new IVeldtService WithDeadline(System.DateTime deadline)
        {
            return base.WithDeadline(deadline);
        }

        public new IVeldtService WithHost(string host)
        {
            return base.WithHost(host);
        }

        public new IVeldtService WithOptions(CallOptions option)
        {
            return base.WithOptions(option);
        }
   
        static ResponseContext _GenerateException(RequestContext __context)
        {
            return CreateResponseContext<string, global::MessagePack.Nil>(__context, GenerateExceptionMethod);
        }

        public global::MagicOnion.UnaryResult<global::MessagePack.Nil> GenerateException(string message)
        {
            return InvokeAsync<string, global::MessagePack.Nil>("IVeldtService/GenerateException", message, GenerateExceptionDelegate);
        }
        static ResponseContext _SendReportAsync(RequestContext __context)
        {
            return CreateResponseContext<string, global::MessagePack.Nil>(__context, SendReportAsyncMethod);
        }

        public global::MagicOnion.UnaryResult<global::MessagePack.Nil> SendReportAsync(string message)
        {
            return InvokeAsync<string, global::MessagePack.Nil>("IVeldtService/SendReportAsync", message, SendReportAsyncDelegate);
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace ServerShared.Hubs {
    using Grpc.Core;
    using MagicOnion;
    using MagicOnion.Client;
    using MessagePack;
    using System;
    using System.Threading.Tasks;

    [Ignore]
    public class VeldtHubClient : StreamingHubClientBase<global::ServerShared.Hubs.IVeldtHub, global::ServerShared.Hubs.IVeldtHubReceiver>, global::ServerShared.Hubs.IVeldtHub
    {
        static readonly Method<byte[], byte[]> method = new Method<byte[], byte[]>(MethodType.DuplexStreaming, "IVeldtHub", "Connect", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);

        protected override Method<byte[], byte[]> DuplexStreamingAsyncMethod { get { return method; } }

        readonly global::ServerShared.Hubs.IVeldtHub __fireAndForgetClient;

        public VeldtHubClient(CallInvoker callInvoker, string host, CallOptions option, MessagePackSerializerOptions serializerOptions, IMagicOnionClientLogger logger)
            : base(callInvoker, host, option, serializerOptions, logger)
        {
            this.__fireAndForgetClient = new FireAndForgetClient(this);
        }
        
        public global::ServerShared.Hubs.IVeldtHub FireAndForget()
        {
            return __fireAndForgetClient;
        }

        protected override void OnBroadcastEvent(int methodId, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -1297457280: // OnJoin
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    receiver.OnJoin(); break;
                }
                case 532410095: // OnLeave
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    receiver.OnLeave(); break;
                }
                default:
                    break;
            }
        }

        protected override void OnResponseEvent(int methodId, object taskCompletionSource, ArraySegment<byte> data)
        {
            switch (methodId)
            {
                case -733403293: // JoinAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 1368362116: // LeaveAsync
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case 517938971: // GenerateException
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                case -852153394: // SampleMethod
                {
                    var result = MessagePackSerializer.Deserialize<Nil>(data, serializerOptions);
                    ((TaskCompletionSource<Nil>)taskCompletionSource).TrySetResult(result);
                    break;
                }
                default:
                    break;
            }
        }
   
        public global::System.Threading.Tasks.Task JoinAsync(global::ServerShared.MessagePackObjects.JoinRequest request)
        {
            return WriteMessageWithResponseAsync<global::ServerShared.MessagePackObjects.JoinRequest, Nil>(-733403293, request);
        }

        public global::System.Threading.Tasks.Task LeaveAsync()
        {
            return WriteMessageWithResponseAsync<Nil, Nil>(1368362116, Nil.Default);
        }

        public global::System.Threading.Tasks.Task GenerateException(string message)
        {
            return WriteMessageWithResponseAsync<string, Nil>(517938971, message);
        }

        public global::System.Threading.Tasks.Task SampleMethod(global::System.Collections.Generic.List<int> sampleList, global::System.Collections.Generic.Dictionary<int, string> sampleDictionary)
        {
            return WriteMessageWithResponseAsync<DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>, Nil>(-852153394, new DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>(sampleList, sampleDictionary));
        }

        [Ignore]
        class FireAndForgetClient : global::ServerShared.Hubs.IVeldtHub
        {
            readonly VeldtHubClient __parent;

            public FireAndForgetClient(VeldtHubClient parentClient)
            {
                this.__parent = parentClient;
            }

            public global::ServerShared.Hubs.IVeldtHub FireAndForget()
            {
                throw new NotSupportedException();
            }

            public Task DisposeAsync()
            {
                throw new NotSupportedException();
            }

            public Task WaitForDisconnect()
            {
                throw new NotSupportedException();
            }

            public global::System.Threading.Tasks.Task JoinAsync(global::ServerShared.MessagePackObjects.JoinRequest request)
            {
                return __parent.WriteMessageAsync<global::ServerShared.MessagePackObjects.JoinRequest>(-733403293, request);
            }

            public global::System.Threading.Tasks.Task LeaveAsync()
            {
                return __parent.WriteMessageAsync<Nil>(1368362116, Nil.Default);
            }

            public global::System.Threading.Tasks.Task GenerateException(string message)
            {
                return __parent.WriteMessageAsync<string>(517938971, message);
            }

            public global::System.Threading.Tasks.Task SampleMethod(global::System.Collections.Generic.List<int> sampleList, global::System.Collections.Generic.Dictionary<int, string> sampleDictionary)
            {
                return __parent.WriteMessageAsync<DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>>(-852153394, new DynamicArgumentTuple<global::System.Collections.Generic.List<int>, global::System.Collections.Generic.Dictionary<int, string>>(sampleList, sampleDictionary));
            }

        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
