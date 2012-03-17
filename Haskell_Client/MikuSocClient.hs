import Network
import System.IO 
import Control.Exception
import Prelude hiding (catch)

sendMessage :: String -> IO ()
sendMessage msg = withSocketsDo $ do 
        hSetBuffering stdout NoBuffering 
        h <- connectTo "127.0.0.1" (PortNumber 3939)
        hSetEncoding h utf8
        hSetBuffering h LineBuffering
        hPutStrLn h msg
        catch (hGetLine h)
              (\e -> return (e::SomeException) >> return "send done.")
              >>= putStrLn
        hClose h


checkLengthAndTwit msg 
 | 140 < length msg = putStrLn "over 140 characters!!"
 | otherwise = sendMessage msg
 
main = do putStrLn "Input Tweet"
          msg <- getLine
          checkLengthAndTwit msg