Module GPTModule
    Public CurrentGPTSetting As GTPModel
    Public GPTAPI As OpenAI_API.OpenAIAPI


End Module


'Static Async Task<String> FixText(String text)
'{
'    Try
'    {
'        OpenAI_API.OpenAIAPI api = New OpenAI_API.OpenAIAPI("sk-YOUR_API_KEY_HERE");

'        String result = text;

'        var chat = api.Chat.CreateConversation();
'        chat.Model = Model.GPT4_Turbo; // Note: Ensure Model Enum Is correctly referenced based On OpenAI API
'        chat.AppendSystemMessage("Please fix data getting from OCR and get ID.No, DOB, Expiry, Name, Arabic Name, Nationality, occupation Arabic, Passport Number, Passport expiry, employer and sex (try to predict it from name) and ResidencyType and also add ResidencyTypeEnglish (TransalateArabic word to english)  and age (calculat it ) and make it in JSON format in your response. Just give JSON, no need for other text.");
'        chat.AppendUserInput(result);
'        String response = await chat.GetResponseFromChatbotAsync();
'        Return response;
'    }
'    Catch (Exception ex)
'    {
'        Return "";
'    }
'}
