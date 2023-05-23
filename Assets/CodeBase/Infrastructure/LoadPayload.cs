﻿namespace CodeBase.Infrastructure
{
    public readonly struct LoadPayload
    {
        public readonly string SceneName;

        public LoadPayload(string sceneName) =>
            SceneName = sceneName;
    }
}