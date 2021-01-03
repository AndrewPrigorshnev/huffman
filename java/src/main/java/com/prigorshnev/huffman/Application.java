package com.prigorshnev.huffman;
import picocli.CommandLine;
import picocli.CommandLine.*;

import java.util.concurrent.Callable;

public class Application implements Callable<Integer> {
    @Parameters(index = "0")
    String command;

    @Parameters(index = "1")
    String source;

    @Parameters(index = "2")
    String target;

    @Override
    public Integer call() throws Exception {
        return null;
    }

    public static void main(String[] args) {
        Application application = new Application();
        int exitCode = new CommandLine(application.execute(args));
        System.exit(exitCode);
    }
}
