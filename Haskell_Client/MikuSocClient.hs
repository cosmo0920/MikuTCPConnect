import Network
import System.IO
import System.IO.Error
import Prelude hiding (catch)

sendMessage :: String -> IO ()
sendMessage msg = withSocketsDo $ do 
        hSetBuffering stdout NoBuffering 
        h <- connectTo "127.0.0.1" (PortNumber 12345)
        hSetEncoding h utf8
        hSetBuffering h LineBuffering
        hPutStrLn h msg
        catch (hGetLine h)
              (\e -> checkEOFOrError e)
              >>= putStrLn
        hClose h

-- isEOFErrorは見なかったことにして終了
checkEOFOrError ex = if isEOFError ex then 
                         return () >> return "send done."
                     else ioError ex >> return "IOerror."

-- 文字数のチェック
-- TODO:URLの文字数カウント
checkLengthAndTwit msg
 | 0 == length msg = putStrLn "at least 1 character!" 
 | 140 < length msg = putStrLn "over 140 characters!!"
 | otherwise = sendMessage msg
 
main = do putStrLn "Input Tweet"
          msg <- getLine
          checkLengthAndTwit msg
