using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeneGames.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public bool allowTriggerOnce=false;
        private int currentSentence;
        private float coolDownTimer;
        private bool dialogueIsOn=false;
        private bool dialogueTriggeredOnce=false;
        public enum TriggerState
        {
            Collision,
            Input
        }

        [Header("References")]
        [SerializeField] private AudioSource audioSource;

        [Header("Events")]
        public UnityEvent startDialogueEvent;
        public UnityEvent nextSentenceDialogueEvent;
        public UnityEvent endDialogueEvent;

        [Header("Dialogue")]
        [SerializeField] private TriggerState triggerState;
        [SerializeField] private List<NPC_Centence> sentences = new List<NPC_Centence>();

        private void Update()
        {
            //Timer
            if(coolDownTimer > 0f)
            {
                coolDownTimer -= Time.deltaTime;
            }

            // //Start dialogue by input
            // Debug.Log(DialogueUI.instance.actionInput);
            // if (Input.GetMouseButtonDown(0) && !dialogueIsOn)
            // // if (Input.GetMouseButtonDown(DialogueUI.instance.actionInput) && !dialogueIsOn)
            // // if (Input.GetMouseButtonDown(DialogueUI.instance.actionInput) && !dialogueIsOn)
            // // if (Input.GetKeyDown(DialogueUI.instance.actionInput) && dialogueTrigger != null && !dialogueIsOn)
            // {
            //     // //Trigger event inside DialogueTrigger component
            //     // if (dialogueTrigger != null)
            //     // {
            //     //     dialogueTrigger.startDialogueEvent.Invoke();
            //     // }
            //     Debug.Log("Manager update invoke ");
            //     startDialogueEvent.Invoke();

            //     //If component found start dialogue
            //     DialogueUI.instance.StartDialogue(this);

            //     //Hide interaction UI
            //     DialogueUI.instance.ShowInteractionUI(false);

            //     dialogueIsOn = true;
            // }
            // DialogueUI.instance.ShowInteractionUI(false);
        }

        private void OnMouseDown()
        {
            // Check if dialogue is currently off and it has not been triggered before
            if ((!dialogueTriggeredOnce|!allowTriggerOnce) && !dialogueIsOn)
            {
                startDialogueEvent.Invoke();
                //If component found start dialogue
                DialogueUI.instance.StartDialogue(this);
                dialogueIsOn = true;
                dialogueTriggeredOnce = true;
            }
        }

        // //Start dialogue by pressing DialogueUI action input
        // private void OnTriggerStay(Collider other)
        // {
        //     if (triggerState == TriggerState.Input)
        //     {
        //         //Show interaction UI
        //         DialogueUI.instance.ShowInteractionUI(true);
        //     }
        // }

        // private void OnTriggerExit(Collider other)
        // {
        //     //Hide interaction UI
        //     DialogueUI.instance.ShowInteractionUI(false);

        //     //Stop dialogue
        //     StopDialogue();
        // }
        public void StartDialogue()
        {
            //Cooldown timer
            coolDownTimer = 0.5f;

            //Start event
            // if(dialogueTrigger != null)
            // {
            //     dialogueTrigger.startDialogueEvent.Invoke();
            // }
            startDialogueEvent.Invoke();

            //Reset sentence index
            currentSentence = 0;

            //Show first sentence in dialogue UI
            ShowCurrentSentence();

            //Play dialogue sound
            PlaySound(sentences[currentSentence].sentenceSound);
        }

        public void NextSentence(out bool lastSentence)
        {
            //The next sentence cannot be changed immediately after starting
            if (coolDownTimer > 0f)
            {
                lastSentence = false;
                return;
            }

            //Add one to sentence index
            currentSentence++;

            // //Next sentence event
            // if (dialogueTrigger != null)
            // {
            //     dialogueTrigger.nextSentenceDialogueEvent.Invoke();
            // }

            nextSentenceDialogueEvent.Invoke();

            //If last sentence stop dialogue and return
            if (currentSentence > sentences.Count - 1)
            {
                StopDialogue();

                lastSentence = true;

                return;
            }

            //If not last sentence continue...
            lastSentence = false;

            //Play dialogue sound
            PlaySound(sentences[currentSentence].sentenceSound);

            //Show next sentence in dialogue UI
            ShowCurrentSentence();

            //Cooldown timer
            coolDownTimer = 0.5f;
        }

        public void StopDialogue()
        {
            endDialogueEvent.Invoke();

            //Hide dialogue UI
            DialogueUI.instance.ClearText();

            //Stop audiosource so that the speaker's voice does not play in the background
            if(audioSource != null)
            {
                audioSource.Stop();
            }

            //Remove trigger refence
            dialogueIsOn = false;
        }

        private void PlaySound(AudioClip _audioClip)
        {
            //Play the sound only if it exists
            if (_audioClip == null || audioSource == null)
                return;

            //Stop the audioSource so that the new sentence does not overlap with the old one
            audioSource.Stop();

            //Play sentence sound
            audioSource.PlayOneShot(_audioClip);
        }

        private void ShowCurrentSentence()
        {
            if (sentences[currentSentence].dialogueCharacter != null)
            {
                //Show sentence on the screen
                DialogueUI.instance.ShowSentence(sentences[currentSentence].dialogueCharacter, sentences[currentSentence].sentence);

                //Invoke sentence event
                sentences[currentSentence].sentenceEvent.Invoke();
            }
            else
            {
                DialogueCharacter _dialogueCharacter = new DialogueCharacter();
                _dialogueCharacter.characterName = "";
                _dialogueCharacter.characterPhoto = null;

                DialogueUI.instance.ShowSentence(_dialogueCharacter, sentences[currentSentence].sentence);
            }
        }
    }

    [System.Serializable]
    public class NPC_Centence
    {
        [Header("------------------------------------------------------------")]

        public DialogueCharacter dialogueCharacter;

        [TextArea(3, 10)]
        public string sentence;

        public AudioClip sentenceSound;

        public UnityEvent sentenceEvent;
    }
}