using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.S3;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3.Model;
using System.IO;
using UnityEditor.PackageManager;
using Amazon.Runtime;

public class AWS_S3 : MonoBehaviour
{
    string S3BucketName = "stagedata-tripletile";

    string IdentityPoolId = "ap-northeast-2:79400e71-0a49-4f8b-9692-351e60a1d156";
    public string CognitoIdentityRegion = RegionEndpoint.APNortheast2.SystemName;

    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }

    public string S3Region = RegionEndpoint.APNortheast2.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }

    private IAmazonS3 _s3Client;

    private AWSCredentials _credentials;

    private AWSCredentials Credentials
    {
        get
        {
            if (_credentials == null)
            {
                _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
            }
            return _credentials;
        }
    }


    private IAmazonS3 Client
    {
        get
        {
            if (_s3Client == null)
            {
                _s3Client = new AmazonS3Client(Credentials, _S3Region);
            }
            //test comment
            return _s3Client;
        }
    }

    private void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
    }

    //public void PostObject(int stageIndex)
    //{
    //    string fileName = stageIndex + ".json";
    //    var stream = new FileStream(Application.dataPath + "/StageData" + fileName
    //        , FileMode.Open, FileAccess.Read, FileShare.Read);
    //    var request = new PostObjectRequest()
    //    {
    //        Bucket = S3BucketName,
    //        Key = fileName,
    //        InputStream = stream,
    //        CannedACL = S3CannedACL.Private
    //    };

    //    Client.PostObjectAsync(request, (responseObj) =>
    //    {
    //        if (responseObj.Exception == null)
    //        {
    //            Debug.Log(string.Format("\nobject {0} posted to bucket {1}",
    //            responseObj.Request.Key, responseObj.Request.Bucket));
    //        }
    //        else
    //        {
    //            Debug.Log("\nException while posting the result object");
    //            Debug.Log(string.Format("\n receieved error {0}",
    //            responseObj.Response.HttpStatusCode.ToString()));
    //        }
    //    });
    //}
    public void PutObject(int stageIndex)
    {
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        Debug.Log("Retrieving the file");

        string fileName = "StageData" + stageIndex + ".json";

        var stream = new FileStream(Application.dataPath + "/Json/" + fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

        Debug.Log("\nCreating request object");
        var request = new PutObjectRequest()
        {
            BucketName = "stagedata-tripletile",
            Key = fileName,
            InputStream = stream,
            CannedACL = S3CannedACL.Private
        };

        Debug.Log("\nMaking HTTP post call");
        Client.PutObjectAsync(request, (responseObj) =>
        {
            if (responseObj.Exception == null)
            {
                Debug.Log(string.Format("\nobject {0} puted to bucket {1}", responseObj.Request.Key, responseObj.Request.BucketName));
            }
            else
            {
                Debug.Log("\nException while puting the result object");
                Debug.Log(string.Format("\n receieved error {0}", responseObj.Response.HttpStatusCode.ToString()));
            }
        });
    }



    private void GetObject(int stageIndex)
    {
        string fileName = "StageData" + stageIndex + ".json";
        string BucketName = "stagedata-tripletile";
        Debug.Log(string.Format("fetching {0} from bucket {1}",
        fileName, BucketName));
        Client.GetObjectAsync(fileName, BucketName, (responseObj) =>
        {
            string data = null;
            var response = responseObj.Response;
            if (response.ResponseStream != null)
            {
                using (StreamReader reader = new StreamReader(response.ResponseStream))
                {
                    data = reader.ReadToEnd();
                }

                Debug.Log("\n" + data);
            }
        });
    }
}
