== Symmetric Encryption Pipeline Components

CryptoPipeline is a custom pipeline component for BizTalk Server 2006 that 
provides encoding and decoding components for symmetric encryption
algorithms like DES, 3DES, Rindjael and RC2.

== Using the Components

Both encoder/decoder components have to configurable properties:

   - Algorith: The encryption algorithm to use
   - SsoConfigApp: The name of a configuration application in Enterprise
     Single Sign-On that contains the encryption key and initialization
     vector (IV) to use.

Included with the components is a small Windows Forms application 
(CryptoConfig) that can be used to create and manage the Key and IV stored in
ENTSSO easily.

== More Information

More information can be found here:
http://winterdom.com/weblog/2006/04/13/SymmetricEncryptionDecryptionPipelineComponents.aspx

