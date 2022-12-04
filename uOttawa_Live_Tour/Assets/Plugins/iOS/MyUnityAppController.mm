    // To fix the audio beign muted if phone is on silent
    // Answer from: https://forum.unity.com/threads/problem-with-mute-button-ios-silent-button.387579/

    #import "UnityAppController.h"
    #import "AVFoundation/AVFoundation.h"
     
    @interface MyUnityAppController: UnityAppController {}
     
    -(void)setAudioSession;
     
    @end
     
    @implementation MyUnityAppController
     
    -(void) startUnity: (UIApplication*) application
    {
        NSLog(@"MyUnityAppController startUnity");
        [super startUnity: application];  //call the super.
        [self setAudioSession];
    }
     
    - (void)setAudioSession
    {
        NSLog(@"MyUnityAppController Set audiosession");
        AVAudioSession *audioSession = [AVAudioSession sharedInstance];
        [audioSession setCategory:AVAudioSessionCategoryPlayback error:nil];
        [audioSession setActive:YES error:nil];
    }
     
    @end
     
    IMPL_APP_CONTROLLER_SUBCLASS(MyUnityAppController)
