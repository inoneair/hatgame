using System.Collections;
using System.Collections.Generic;
using Hatgame.Multiplayer;
using UnityEngine;
using Mirror;

public class MultiPlayerEntity
{
    private CreateOrJoinLobbyMenuLogic _createOrJoinLobbyMenuLogic;

    public MultiPlayerEntity(CreateOrJoinLobbyMenuView createOrJoinLobbyMenuView, string serverAddress, Transport networkTransport)
    {
        NetworkController.instance.transport = networkTransport;
        NetworkController.instance.SetNetworkAddress(serverAddress);

        NetworkController.instance.SubscribeOnClientConnect(OnConnectedHandler);
        NetworkController.instance.SubscribeOnClientDisconnect(OnDisconnectedHandler);

        _createOrJoinLobbyMenuLogic = new CreateOrJoinLobbyMenuLogic(createOrJoinLobbyMenuView);

        _createOrJoinLobbyMenuLogic.SubscribeOnShow(TryToEstablishConnection);
        _createOrJoinLobbyMenuLogic.SubscribeOnReturn(StopConnection);
    }

    public IMenuLogic createOrJoinLobbyMenuLogic => _createOrJoinLobbyMenuLogic;

    private void TryToEstablishConnection()
    {
        NetworkController.instance.StartClient();
    }

    private void StopConnection()
    {
        NetworkController.instance.StopClient();
    }

    private void OnConnectedHandler()
    {
        Debug.Log("OnConnected");
    }

    private void OnDisconnectedHandler()
    {
        Debug.Log("OnDisconnected");
    }
}
