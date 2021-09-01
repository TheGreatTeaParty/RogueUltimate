using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public static class GPGSManger
{
    public const string DEFAULT_SAVE_NAME = "com.thegreatteaparty.roguestales.skin";

    private static ISavedGameClient savedGameClient;
    private static ISavedGameMetadata currentMetaData;

    public static bool IsAuthenticated
    {
        get
        {
            if (PlayGamesPlatform.Instance != null)
                return PlayGamesPlatform.Instance.IsAuthenticated();
            return false;
        }
    }

    public static void Initialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

    }

    public static void Auth(Action<bool> onAuth)
    {
        Social.localUser.Authenticate((success) =>
        {
            if (success) savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        });
    }

    private static void OpenSaveData(string fileName, Action<SavedGameRequestStatus, ISavedGameMetadata> onDataOpen)
    {
        if (!IsAuthenticated)
        {
            onDataOpen(SavedGameRequestStatus.AuthenticationError, null);
            return;
        }
        savedGameClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseMostRecentlySaved, onDataOpen);
    }

    public static void ReadSaveData(string fileName, Action<SavedGameRequestStatus,byte[]> onDataRead)
    {
        if (!IsAuthenticated)
        {
            onDataRead(SavedGameRequestStatus.AuthenticationError, null);
            return;
        }
        OpenSaveData(fileName, (status, metadata) =>
        {
            if(status == SavedGameRequestStatus.Success)
            {
                savedGameClient.ReadBinaryData(metadata, onDataRead);
                currentMetaData = metadata;
            }
        });
    }

    public static void WriteSaveData(byte[] data)
    {
        if (!IsAuthenticated || data == null || data.Length == 0)
            return;
        Action onDataWrite = () =>
        {
            SavedGameMetadataUpdate updatedMetaData = new SavedGameMetadataUpdate.Builder().Build();
            savedGameClient.CommitUpdate(currentMetaData,
                updatedMetaData,
                data,
                (status, metadata) => currentMetaData = metadata);
        };
        if(currentMetaData == null)
        {
            OpenSaveData(DEFAULT_SAVE_NAME, (status, metadata) =>
            {
                if(status == SavedGameRequestStatus.Success)
                {
                    currentMetaData = metadata;
                    onDataWrite();
                }
            });
            return;
        }
        onDataWrite();
    }
}
