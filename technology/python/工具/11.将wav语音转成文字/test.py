import json
from os.path import join, dirname
from ibm_watson import SpeechToTextV1
from ibm_watson.websocket import RecognizeCallback, AudioSource
from ibm_cloud_sdk_core.authenticators import IAMAuthenticator

authenticator = IAMAuthenticator('1C9Vw21m3m66UHmo4QQnurkaNVOQaADnjLwfihTWx1Sq')
speech_to_text = SpeechToTextV1(
    authenticator=authenticator
)

speech_to_text.set_service_url(
    'https://api.us-south.speech-to-text.watson.cloud.ibm.com/instances/12c07f09-80dd-47cf-b341-7b2e9c603d9a')


class MyRecognizeCallback(RecognizeCallback):
    def __init__(self):
        RecognizeCallback.__init__(self)

    def on_data(self, data):
        print(json.dumps(data, indent=2))

    def on_error(self, error):
        print('Error received: {}'.format(error))

    def on_inactivity_timeout(self, error):
        print('Inactivity timeout: {}'.format(error))


myRecognizeCallback = MyRecognizeCallback()

with open(join(dirname(__file__), './.', 'output_audio.wav'),
          'rb') as audio_file:
    audio_source = AudioSource(audio_file)
    speech_to_text.recognize_using_websocket(
        audio=audio_source,
        content_type='audio/wav',
        recognize_callback=myRecognizeCallback,
        model='zh-CN_BroadbandModel',
        max_alternatives=3)