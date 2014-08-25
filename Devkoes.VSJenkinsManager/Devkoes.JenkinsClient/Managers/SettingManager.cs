﻿using Devkoes.JenkinsClient.Model;
using Devkoes.JenkinsClient.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Devkoes.JenkinsClient.Managers
{
    public static class SettingManager
    {
        static SettingManager()
        {
            if (Settings.Default.SolutionJobs == null)
            {
                Settings.Default.SolutionJobs = new SolutionJobList();
            }

            if (Settings.Default.JenkinsServers == null)
            {
                Settings.Default.JenkinsServers = new JenkinsServerList();
            }

            Settings.Default.Save();

        }

        public static void SaveJobForSolution(string jobUri, string solutionPath, string jenkinsServerUri)
        {
            var existingSolutionJob = Settings.Default.SolutionJobs.FirstOrDefault((sj) => string.Equals(sj.SolutionPath, solutionPath, StringComparison.InvariantCultureIgnoreCase));
            if (existingSolutionJob != null)
            {
                Settings.Default.SolutionJobs.Remove(existingSolutionJob);
            }

            Settings.Default.SolutionJobs.Add(new SolutionJob()
            {
                SolutionPath = solutionPath,
                JobUrl = jobUri,
                JenkinsServerUrl = jenkinsServerUri
            });

            Settings.Default.Save();
        }

        public static SolutionJob GetJobUri(string solutionPath)
        {
            return Settings.Default.SolutionJobs.FirstOrDefault((sj) => string.Equals(sj.SolutionPath, solutionPath, StringComparison.InvariantCultureIgnoreCase));
        }

        public static JenkinsServer GetJenkinsServer(string solutionUrl)
        {
            return Settings.Default.JenkinsServers.FirstOrDefault((js) => string.Equals(js.Url, solutionUrl, StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool ContainsSolutionPreference(string solutionPath)
        {
            return Settings.Default.SolutionJobs.Any((sj) => string.Equals(sj.SolutionPath, solutionPath, StringComparison.InvariantCultureIgnoreCase));
        }

        public static void AddServer(JenkinsServer server)
        {
            Settings.Default.JenkinsServers.Add(server);
            Settings.Default.Save();
        }

        public static IEnumerable<JenkinsServer> GetServers()
        {
            return Settings.Default.JenkinsServers.ToArray();
        }

        public static void RemoveServer(JenkinsServer server)
        {
            Settings.Default.JenkinsServers.Remove(server);
            Settings.Default.Save();
        }
    }
}
