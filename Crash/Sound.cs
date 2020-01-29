//  Date: 1-24-2020
//  C# Mini Project - Cras# a Text Adventure
//  The Dextrous Devs - Jing Xie, Matt Juel, Radiah Jones
//  Purpose: Plays audio files in project and disposes of resources when complete.
using NAudio.Wave;
using System.Threading;

namespace Crash
{
    internal static class Sound
    {
        internal static AudioFileReader audioFile;
        internal static WaveOutEvent outputDevice;

        // plays the specified sound file. If a milliSecond is also provided, that is used in the Thread.Sleep()
        internal static void PlaySound(string musicFile, int milliSeconds = 0)
        {
            // initialize audio device
            audioFile = new AudioFileReader(musicFile);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);

            // play and sleep after, if requested
            outputDevice.Play();
            Thread.Sleep(milliSeconds);
        }

        // dispose of unmanaged audio resources
        internal static void DisposeAudio()
        {
            audioFile.Dispose();
            outputDevice.Dispose();
        }
    }
}